using Application.Abstractions.Messaging;

namespace Application.Usuarios.ConfirmarCorreo;

public record ConfirmarCorreoCommand(string Token, string CorreoElectronico) : ICommand<bool>;
