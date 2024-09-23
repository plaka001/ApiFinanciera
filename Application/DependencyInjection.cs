using Application.Abstractions.Behaviors;
using Application.Usuarios.ConfirmarCorreo;
using Application.Usuarios.RegistrarUsuario;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });
        services.AddScoped<IValidator<RegisterUsuarioCommand>, RegisterUsuarioCommandValidator>();
        services.AddScoped<IValidator<ConfirmarCorreoCommand>, ConfirmarCorreoCommandValidator>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
