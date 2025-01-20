namespace Application.Categorias.ActualizarCategoria;

public record ActualizarCategoriaRequest(Guid CategoriaId, string Nombre, Guid UserId, int TipoCategoria);
