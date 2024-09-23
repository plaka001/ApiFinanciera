using Domain.Abstractions;

namespace Domain.Email;

public class EmailErrors
{
    public static Error SendEmail = new(
        "Email.Send",
        "Error al enviar el condigo de confirmacion correo"
    );

}

