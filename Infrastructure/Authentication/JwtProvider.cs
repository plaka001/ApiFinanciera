using Application.Abstractions.Authentication;
using Domain.Usuarios;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public Task<string> GenerarJwt(Usuario user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id!.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.CorreoElectronico!.ToString())
        };

        var siginCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey!)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.UtcNow.AddDays(1), siginCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult<string>(tokenValue);
    }
}
