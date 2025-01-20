using Application.Abstractions.Messaging;

namespace Application.Categorias.ObtenerCategorias;

public sealed record CategoriasQuery(Guid usuarioId) : IQuery<IReadOnlyList<CategoriaResponse>>;

