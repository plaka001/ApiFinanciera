using Domain.Abstractions;

namespace Domain.Usuarios;

public class UsuarioErrores
{
    public static Error NotFound = new(
        "User.Found",
        "No existe el usuario"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Las credenciales son incorrectas intentos {0} de 3"
    );

    public static Error UserAlreadyRegister = new(
        "User.AlreadyRegister",
        "El usuario ya se encuentra registrado"
    );

    public static Error Blocked = new(
        "User.Blocked",
        "El usuario se encuentra bloqueado hasta {0}"
    );

    public static Error InvalidToken = new(
            "User.InvalidToken",
            "El token es invalido"
    );

    public static Error EmailAlreadyConfirmed = new(
        "User.EmailAlreadyConfirmed",
        "El correo ya se encuentra confirmado"
    );

    public static Error TokeExpired = new(
        "User.TokenExpired",
        "El token ha expirado"
    );

    public static Error CorreoYaConfirmado = new(
        "User.CorreoYaConfirmado",
        "El correo ya se encuentra confirmado"
    );

}
