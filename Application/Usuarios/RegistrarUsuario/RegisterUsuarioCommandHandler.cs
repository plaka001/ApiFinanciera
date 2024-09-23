using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;

namespace Application.Usuarios.RegistrarUsuario;

public sealed class RegisterUsuarioCommandHandler : ICommandHandler<RegisterUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerSegunCorreo(new CorreoElectronico(request.Email), cancellationToken);
        if (usuario != null) return Result.Failure<Guid>(UsuarioErrores.UserAlreadyRegister);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);
        var usuarioCreate = Usuario.Create(new Nombre(request.Nombre), new CorreoElectronico(request.Email), new Contrasena(passwordHash),new IntentosLogin(0));

        _usuarioRepository.Agregar(usuarioCreate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(usuarioCreate.Id!.Value);
    }
}
