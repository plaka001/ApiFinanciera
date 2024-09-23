using Application.Abstractions.Messaging;
using Domain.Usuarios.ObjectValues;

namespace Application.Usuarios.RefrescarToken;

public record RefrescarTokenCommand(CorreoElectronico CorreoElectronico): ICommand<string>;

