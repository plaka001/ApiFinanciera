using Application.Abstractions.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Email;

public sealed class EmailService : IEmailService
{
    private readonly IOptionsMonitor<EmailSettings> _settingsMonitor;

    public EmailService(IOptionsMonitor<EmailSettings> settingsMonitor)
    {
        _settingsMonitor = settingsMonitor;
    }

    public async Task SendAsyncTokenEmail(string body, string to, string subject)
    {
        var configuracionEmail = _settingsMonitor.CurrentValue;
        using var smtpClient = new SmtpClient(configuracionEmail.Host)
        {
            Port = 587,
            Credentials = new NetworkCredential(configuracionEmail.UsuarioEmail, configuracionEmail.UsuarioPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage(configuracionEmail.UsuarioEmail!, to, subject, body)
        {
            IsBodyHtml = true
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
