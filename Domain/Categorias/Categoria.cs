using Domain.Abstractions;
using Domain.Categorias.ObjectValues;
using Domain.Usuarios.ObjectValues;

namespace Domain.Categorias;

public sealed class Categoria : Entity<CategoriaId>
{
    public Categoria() { }

    private Categoria(CategoriaId id, NombreCategoria nombre, UsuarioId usuarioId, TipoCategoria tipo, DateTime fechaCreacion) : base(id)
    {
        Nombre = nombre;
        UsuarioId = usuarioId;
        Tipo = tipo;
        FechaCreacion = fechaCreacion;
    }

    public NombreCategoria? Nombre { get; set; }
    public TipoCategoria Tipo { get; private set; }
    public UsuarioId? UsuarioId { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public static Categoria Crear(NombreCategoria nombreCategoria, UsuarioId usuarioId, TipoCategoria tipoCategoria)
    {
        var categoria = new Categoria(CategoriaId.New(), nombreCategoria, usuarioId, tipoCategoria, DateTime.Now);
        return categoria;
    }

    public static Categoria Actualizar(Categoria categoria, NombreCategoria nombreCategoria, TipoCategoria tipoCategoria)
    {
        categoria.Nombre = nombreCategoria;
        categoria.Tipo = tipoCategoria;
        return categoria;
    }

}
