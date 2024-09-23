using Domain.Abstractions;
using Domain.Email;
using Domain.Usuarios.Events;
using Domain.Usuarios.ObjectValues;

namespace Domain.Usuarios;
public sealed class Usuario : Entity<UsuarioId>
{
    public Usuario() { }

    private Usuario(
                   UsuarioId id,
                   Nombre nombre,
                   CorreoElectronico correoElectronico,
                   Contrasena contrasena,
                   DateTime fechaRegistro,
                   IntentosLogin intentosLogin,
                   DateTime fechaBloqueoLogin,
                   TokenCorreo tokenCorreo,
                   DateTime fechaExpiraToken,
                   EmailEstados emailEstados
        ) : base(id)
    {
        Nombre = nombre;
        CorreoElectronico = correoElectronico;
        Contrasena = contrasena;
        FechaRegistro = fechaRegistro;
        IntentosLogin = intentosLogin;
        FechaBloqueoLogin = fechaBloqueoLogin;
        TokenCorreoConfirmar = tokenCorreo;
        FechaExpiraToken = fechaExpiraToken;
        CorreoConfirmacionEstado = emailEstados;
    }

    public Nombre? Nombre { get; private set; }
    public CorreoElectronico? CorreoElectronico { get; private set; }
    public Contrasena? Contrasena { get; private set; }

    public IntentosLogin? IntentosLogin { get; set; }

    public DateTime FechaBloqueoLogin { get; set; }

    public DateTime FechaRegistro { get; private set; }

    public TokenCorreo? TokenCorreoConfirmar { get; set; }

    public bool ConfirmadoCorreo { get; set; } = false;

    public EmailEstados CorreoConfirmacionEstado { get; set; }

    public DateTime FechaExpiraToken { get; set; }
    public static Usuario Create(
       Nombre nombre,
       CorreoElectronico email,
       Contrasena passwordHash,
       IntentosLogin intentosLogin
        )
    {
        var user = new Usuario(UsuarioId.New(), nombre, email, passwordHash, DateTime.Now, intentosLogin, DateTime.MinValue, new TokenCorreo("0"), DateTime.Now.AddMinutes(3), EmailEstados.NoEnviado);
        user.RaiseDomainEvent(new UsuarioCorreoTokenEvent(user));
        return user;
    }

    public static void CorreoConfirmadoEvent(Usuario usuario)
    {
        usuario.RaiseDomainEvent(new UsuarioCorreoConfirmadoEvent(usuario));
    }


}
