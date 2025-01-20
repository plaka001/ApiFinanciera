namespace Domain.Categorias.ObjectValues;

public record CategoriaId(Guid Value)
{
    public static CategoriaId New() => new(Guid.NewGuid());
}
