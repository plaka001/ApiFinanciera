using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Domain.Usuarios.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario, UsuarioId>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<Usuario?> ObtenerSegunCorreo(CorreoElectronico email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Usuario>()
       .FirstOrDefaultAsync(x => x.CorreoElectronico == email, cancellationToken);
    }
    public async Task<bool> UsuarioExiste(CorreoElectronico email, CancellationToken cancellationToken = default)
    {

        return await DbContext.Set<Usuario>().AnyAsync(x => x.CorreoElectronico == email, cancellationToken); ;
    }
}

