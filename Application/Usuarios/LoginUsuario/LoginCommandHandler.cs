using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;

namespace Application.Usuarios.LoginUsuario;

public sealed class LoginCommandHandler : ICommandHandler<LoginUsuarioCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUsuarioRepository usuarioRepository, IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerSegunCorreo(new CorreoElectronico(request.Email), cancellationToken);

        if (usuario is null) return Result.Failure<string>(UsuarioErrores.NotFound);

        if (ValidarBloqueoUsuario(usuario)) return Result.Failure<string>(UsuarioErrores.Blocked.WithParams(usuario.FechaBloqueoLogin.ToString()));


        if (!BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario!.Contrasena!.Value))
        {
            var intentos = await IncrementarIntentosLogin(usuario);
            if (intentos >= 4)
            {
                await BloquearUsuario(usuario);
                return Result.Failure<string>(UsuarioErrores.Blocked.WithParams(usuario.FechaBloqueoLogin.ToString()));
            }
            return Result.Failure<string>(UsuarioErrores.InvalidCredentials.WithParams(intentos));
        };

        var token = await _jwtProvider.GenerarJwt(usuario);
        return token;
    }


    private static bool ValidarBloqueoUsuario(Usuario usuario)
    {
        return usuario.FechaBloqueoLogin > DateTime.Now;
    }


    private async Task BloquearUsuario(Usuario usuario)
    {
        usuario.IntentosLogin = new IntentosLogin(0);
        usuario.FechaBloqueoLogin = DateTime.Now.AddMinutes(1);
        _usuarioRepository.Actualizar(usuario);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<int> IncrementarIntentosLogin(Usuario usuario)
    {
        usuario.IntentosLogin = new IntentosLogin(usuario.IntentosLogin!.Value + 1);
        _usuarioRepository.Actualizar(usuario);
        await _unitOfWork.SaveChangesAsync();
        return usuario.IntentosLogin!.Value;
    }

}

