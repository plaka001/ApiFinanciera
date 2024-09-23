using Application.Abstractions.Email;
using Domain.Usuarios;
using Domain.Usuarios.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Usuarios.ConfirmarCorreo;

public sealed class UsuarioCorreoConfirmadoEventHandler : INotificationHandler<UsuarioCorreoConfirmadoEvent>
{
    private const string Subject = "¡Confirmación Exitosa! Financiera App";
    private readonly IEmailService _emailService;
    private readonly ILogger<UsuarioCorreoConfirmadoEventHandler> _logger;

    public UsuarioCorreoConfirmadoEventHandler(IEmailService emailService, ILogger<UsuarioCorreoConfirmadoEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(UsuarioCorreoConfirmadoEvent notification, CancellationToken cancellationToken)
    {
        string body = ConstruirBody(notification.Usuario);
        try
        {
            await _emailService.SendAsyncTokenEmail(body, notification.Usuario.CorreoElectronico!.Value, Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al enviar el correo de confirmacion {0}", ex.Message);

        }
    }

    private static string ConstruirBody(Usuario usuario)
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
                                    <h1>¡Confirmación Exitosa!</h1>
                                  </div>
                                  <div class=""content"">
                                    <p>Hola {usuario.Nombre!.Value},</p>
                                    <p>
                                      ¡Tu dirección de correo electrónico ha sido confirmada exitosamente! 
                                      Ahora tienes acceso completo a todas las funcionalidades de Financiera App.
                                    </p>
                                    <p>
                                      Estamos encantados de tenerte a bordo. Si tienes alguna pregunta o necesitas asistencia, 
                                      no dudes en contactarnos.
                                    </p>
                                    <p>
                                      Puedes comenzar a explorar nuestra plataforma en cualquier momento. ¡Esperamos que disfrutes 
                                      de todos los beneficios que ofrecemos!
                                    </p>
                                    <p>Saludos cordiales,<br />El Malón<br />CEO<br />Financiera App</p>
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
