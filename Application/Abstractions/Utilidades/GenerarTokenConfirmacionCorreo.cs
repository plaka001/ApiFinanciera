namespace Application.Abstractions.Utilidades;

public  class GenerarTokenConfirmacionCorreo
{
    public static string GenerarToken()
    {
        Random random = new();
        int token = random.Next(10000, 99999);
        return token.ToString();
    }
}
