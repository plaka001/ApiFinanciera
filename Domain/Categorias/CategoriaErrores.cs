using Domain.Abstractions;

namespace Domain.Categorias;

public class CategoriaErrores
{
    public static Error CategoriaExistente = new(
     "Categoria.Existente",
     "Ya existe la categoria con este nombre"
    );

    public static Error NoEncontrada = new(
        "Categoria.NoEncontrada",
        "La categoria no existe"
        );
}
