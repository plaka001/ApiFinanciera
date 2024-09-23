using Application.Abstractions.Email;
using Application.Abstractions.Utilidades;
using Domain.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;
using MediatR;

namespace Application.Usuarios.RefrescarToken;

public sealed class RefrescarTokenCommandHandler : IRequestHandler<RefrescarTokenCommand, Result<string>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public RefrescarTokenCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(RefrescarTokenCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerSegunCorreo(request.CorreoElectronico);

        if (usuario is null) return Result.Failure<string>(UsuarioErrores.NotFound);
        if (usuario.ConfirmadoCorreo) return Result.Failure<string>(UsuarioErrores.CorreoYaConfirmado);


        var token = GenerarTokenConfirmacionCorreo.GenerarToken();

        usuario.TokenCorreoConfirmar = new TokenCorreo(token);
        usuario.FechaExpiraToken = DateTime.Now.AddMinutes(3);

        _usuarioRepository.Actualizar(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(token);
    }
}

