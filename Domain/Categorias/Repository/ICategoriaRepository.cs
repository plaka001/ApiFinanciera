using Domain.Categorias.ObjectValues;
using Domain.Usuarios.ObjectValues;

namespace Domain.Categorias.Repository;

public interface ICategoriaRepository
{
    public Task<Categoria> ObtenerSegunId(CategoriaId id, CancellationToken cancellationToken = default);
    public void Agregar(Categoria entity);
    public void Actualizar(Categoria entity);
    public Task<Categoria?> ObtenerCategoriaSegunNombreYIdUsuario(NombreCategoria Nombre, UsuarioId usuarioId, TipoCategoria tipoCategoria, CancellationToken CancellationToken = default);
}
