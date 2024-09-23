
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Infrastructure;

namespace Api.Extensions;

public static class SeedDataExtensions
{
    public static async void SeedDataUser(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider;
        var loggerFactory = service.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = service.GetRequiredService<ApplicationDbContext>();
            if (!context.Set<Usuario>().Any())
            {
                var passWordHas = BCrypt.Net.BCrypt.HashPassword("Admin123@");
                var user = Usuario.Create(new Nombre("Admin"), new CorreoElectronico("admin@gmail.com"), new Contrasena(passWordHas), new IntentosLogin(0));
                context.Add(user);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            logger.LogError(ex.Message);
        }
    }
}
