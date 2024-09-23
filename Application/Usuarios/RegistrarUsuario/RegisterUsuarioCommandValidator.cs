using FluentValidation;

namespace Application.Usuarios.RegistrarUsuario;

internal sealed class RegisterUsuarioCommandValidator : AbstractValidator<RegisterUsuarioCommand>
{
    public RegisterUsuarioCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido");

        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
            .Matches("[A-Z]").WithMessage("La contraseña debe tener al menos una mayúscula")
            .Matches("[a-z]").WithMessage("La contraseña debe tener al menos una minúscula")
            .Matches("[0-9]").WithMessage("La contraseña debe tener al menos un número")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe tener al menos un caracter especial");
    }
}
