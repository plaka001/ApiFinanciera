using Domain.Abstractions;

namespace Domain.Usuarios.Events;

public sealed record UsuarioCorreoTokenEvent(Usuario User) : IDomainEvent;
