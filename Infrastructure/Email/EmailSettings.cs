namespace Infrastructure.Email;

public sealed class EmailSettings
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? UsuarioEmail { get; set; }
    public string? UsuarioPassword { get; set; }
}
