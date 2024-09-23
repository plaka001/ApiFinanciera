using Application.Abstractions.Messaging;

namespace Application.Usuarios.RegistrarUsuario;
public record RegisterUsuarioCommand(string Email, string Contrasena, string Nombre) : ICommand<Guid>;

