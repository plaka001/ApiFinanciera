using Domain.Usuarios;

namespace Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<string> GenerarJwt(Usuario user);
}
