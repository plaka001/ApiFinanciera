namespace Domain.Usuarios.ObjectValues;

public record UsuarioId(Guid Value)
{
    public static UsuarioId New() => new(Guid.NewGuid());
}
