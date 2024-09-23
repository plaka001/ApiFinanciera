using Domain.Usuarios.ObjectValues;

namespace Domain.Usuarios.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerSegunId(UsuarioId id, CancellationToken cancellationToken = default);

    void Agregar(Usuario user);

   void Actualizar(Usuario user);

    Task<Usuario?> ObtenerSegunCorreo(CorreoElectronico email, CancellationToken cancellationToken = default);

    Task<bool> UsuarioExiste(CorreoElectronico email, CancellationToken cancellationToken = default);
}
