using Domain.Abstractions;

namespace Domain.Usuarios.Events;

public sealed record UsuarioCorreoConfirmadoEvent(Usuario Usuario) : IDomainEvent;

