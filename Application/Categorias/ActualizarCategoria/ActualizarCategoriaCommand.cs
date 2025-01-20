using Application.Abstractions.Messaging;

namespace Application.Categorias.ActualizarCategoria;

public record class ActualizarCategoriaCommand(Guid CategoriaId, string Nombre, Guid UserId, int TipoCategoria) : ICommand;
