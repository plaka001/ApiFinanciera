using Application.Abstractions.Messaging;

namespace Application.Usuarios.LoginUsuario;

public record LoginUsuarioCommand(string Email, string Contrasena) : ICommand<string>;
