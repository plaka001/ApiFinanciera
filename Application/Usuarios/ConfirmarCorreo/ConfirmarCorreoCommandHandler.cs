using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;

namespace Application.Usuarios.ConfirmarCorreo;

public class ConfirmarCorreoCommandHandler : ICommandHandler<ConfirmarCorreoCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;



    public ConfirmarCorreoCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ConfirmarCorreoCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerSegunCorreo(new CorreoElectronico(request.CorreoElectronico), cancellationToken);
        if (usuario == null) return Result.Failure<bool>(UsuarioErrores.NotFound);

        var resp = ConfirmarTokenValidaciones(usuario, request);
        if (resp.IsFailure) return Result.Failure<bool>(resp.Error);

        usuario.ConfirmadoCorreo = true;
        Usuario.CorreoConfirmadoEvent(usuario);
        _usuarioRepository.Actualizar(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }

    private static Result<bool> ConfirmarTokenValidaciones(Usuario usuario, ConfirmarCorreoCommand request)
    {
        if (usuario.TokenCorreoConfirmar!.Value != request.Token) return Result.Failure<bool>(UsuarioErrores.InvalidToken);
        if (usuario.FechaExpiraToken <= DateTime.Now) return Result.Failure<bool>(UsuarioErrores.TokeExpired);
        if (usuario.ConfirmadoCorreo) return Result.Failure<bool>(UsuarioErrores.EmailAlreadyConfirmed);
        return Result.Success(true);
    }
}
