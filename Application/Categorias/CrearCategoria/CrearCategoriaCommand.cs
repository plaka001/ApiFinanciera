using Application.Abstractions.Messaging;

namespace Application.Categorias.CrearCategoria;

public record  class CrearCategoriaCommand(string Nombre, Guid UserId, int TipoCategoria):ICommand;

