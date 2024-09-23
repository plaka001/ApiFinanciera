using FluentValidation;

namespace Application.Usuarios.ConfirmarCorreo;

internal sealed class ConfirmarCorreoCommandValidator : AbstractValidator<ConfirmarCorreoCommand>
{
    public ConfirmarCorreoCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("El token es requerido");

        RuleFor(x => x.CorreoElectronico).EmailAddress().NotEmpty();
    }
}
