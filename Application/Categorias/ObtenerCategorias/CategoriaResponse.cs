namespace Application.Categorias.ObtenerCategorias;

public sealed class CategoriaResponse
{
    public Guid Id { get; init; }
    public string? Nombre { get; init; }
    public int Tipo { get; init; }
    public Guid UsuarioId { get; init; }
}
