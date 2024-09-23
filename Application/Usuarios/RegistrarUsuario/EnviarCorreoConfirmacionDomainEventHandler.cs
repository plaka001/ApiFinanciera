using Application.Abstractions.Email;
using Application.Abstractions.Utilidades;
using Domain.Abstractions;
using Domain.Email;
using Domain.Usuarios;
using Domain.Usuarios.Events;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Usuarios.RegistrarUsuario;

public sealed class EnviarCorreoConfirmacionDomainEventHandler : INotificationHandler<UsuarioCorreoTokenEvent>
{
    private const string Subject = "Confirmación de correo electrónico";
    private readonly IEmailService _emailService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EnviarCorreoConfirmacionDomainEventHandler> _logger;



    public EnviarCorreoConfirmacionDomainEventHandler(IEmailService emailService, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, ILogger<EnviarCorreoConfirmacionDomainEventHandler> logger)
    {
        _emailService = emailService;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UsuarioCorreoTokenEvent notification, CancellationToken cancellationToken)
    {
        var Token = GenerarTokenConfirmacionCorreo.GenerarToken();
        try
        {
            var body = ConstruirBody(Token, notification.User);
            await _emailService.SendAsyncTokenEmail(body, notification.User.CorreoElectronico!.Value, Subject);

            Usuario user = notification.User;
            user.TokenCorreoConfirmar = new TokenCorreo(Token);
            user.CorreoConfirmacionEstado = EmailEstados.Enviado;

            _usuarioRepository.Actualizar(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Usuario user = notification.User;
            user.CorreoConfirmacionEstado = EmailEstados.Error;
            _usuarioRepository.Actualizar(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogError("Error al enviar el correo de confirmacion {0}", ex.Message);
        }
    }

    private static string ConstruirBody(string token, Usuario usuario)
    {
        string body = $@"<!DOCTYPE html>
                            <html>
                              <head>
                                <meta charset=""UTF-8"" />
                                <style>
                                  body {{
                                    font-family: Arial, sans-serif;
                                    color: #333;
                                    margin: 0;
                                    padding: 0;
                                  }}
                                  .container {{
                                    width: 100%;
                                    max-width: 600px;
                                    margin: 0 auto;
                                    padding: 20px;
                                    background-color: #f9f9f9;
                                    border: 1px solid #ddd;
                                  }}
                                  .header {{
                                    background-color: #4caf50;
                                    color: white;
                                    padding: 10px;
                                    text-align: center;
                                  }}
                                  .content {{
                                    padding: 20px;
                                  }}
                                  .token-box {{
                                    display: inline-block;
                                    font-size: 20px;
                                    color: #4caf50;
                                    font-weight: bold;
                                    padding: 10px;
                                    border: 2px dashed #4caf50;
                                    background-color: #fff;
                                    margin: 20px 0;
                                    text-align: center;
                                  }}
                                  .footer {{
                                    text-align: center;
                                    padding: 10px;
                                    font-size: 0.8em;
                                    color: #777;
                                  }}
                                </style>
                              </head>
                              <body>
                                <div class=""container"">
                                  <div class=""header"">
                                    <h1>Bienvenido a Financiera App</h1>
                                  </div>
                                  <div class=""content"">
                                    <p>Hola {usuario.Nombre!.Value},</p>
                                    <p>
                                      Gracias por registrarte en Financiera App. Nos complace darte la
                                      bienvenida a nuestra comunidad.
                                    </p>
                                    <p>
                                      Para comenzar, por favor ingresa el siguiente código de verificación para confirmar tu
                                      dirección de correo electrónico:
                                    </p>
                                    <div class=""token-box"">
                                      {token}
                                    </div>
                                    <p>
                                      Si prefieres, también puedes hacer clic en el siguiente enlace para confirmar tu correo:
                                    </p>
                                    <p>
                                      <a
                                        style=""color: #4caf50; text-decoration: none""
                                        >Confirmar mi correo</a>
                                    </p>
                                    <p>Si tienes alguna pregunta, no dudes en contactarnos.</p>
                                    <p>Saludos cordiales,<br />Malón<br />CEO<br />Financiera App</p>
                                  </div>
                                  <div class=""footer"">
                                    <p>
                                      Este es un mensaje automático, por favor no respondas a este correo.
                                    </p>
                                    <p>&copy; {DateTime.Now.Year} Financiera App. Todos los derechos reservados.</p>
                                  </div>
                                </div>
                              </body>
                            </html>
                            ";

        return body;
    }
}
