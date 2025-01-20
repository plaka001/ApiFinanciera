using Domain.Categorias;
using Domain.Categorias.ObjectValues;
using Domain.Categorias.Repository;
using Domain.Usuarios.ObjectValues;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class CategoriaRepository : Repository<Categoria, CategoriaId>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Categoria?> ObtenerCategoriaSegunNombreYIdUsuario(NombreCategoria Nombre, UsuarioId usuarioId, TipoCategoria tipoCategoria, CancellationToken CancellationToken = default)
    {
        return await DbContext.Set<Categoria>()
            .FirstOrDefaultAsync(x => x.Nombre == Nombre && x.UsuarioId == usuarioId && x.Tipo == tipoCategoria, CancellationToken);
    }

}