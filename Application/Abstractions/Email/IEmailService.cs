namespace Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsyncTokenEmail(string body, string to, string subject);
}
