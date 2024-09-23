using Application.Usuarios.ConfirmarCorreo;
using Application.Usuarios.LoginUsuario;
using Application.Usuarios.RefrescarToken;
using Application.Usuarios.RegistrarUsuario;
using Domain.Usuarios.ObjectValues;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ISender _sender;

    public UsuarioController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUsuarioCommand(request.Email, request.Contrasena);
        var result = await _sender.Send(command,cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }


    [AllowAnonymous]
    [HttpPost("registrar")]
    public async Task<IActionResult> Register([FromBody] RegisterUsuarioRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUsuarioCommand(request.Email, request.Contrasena, request.Nombre);
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }


    [AllowAnonymous]
    [HttpPost("confirmar-correo")]
    public async Task<IActionResult> ConfirmarCorreo([FromBody] ConfirmarCorreoRequest request, CancellationToken cancellationToken)
    {
        var command = new ConfirmarCorreoCommand(request.Token, request.CorreoElectronico);
        var result = await _sender.Send(command,cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    // Refresh token
    [AllowAnonymous]
    [HttpPost("refrescar-token")]
    public async Task<IActionResult> RefreshToken([FromBody] CorreoElectronico correoElectronico, CancellationToken cancellationToken)
    {
        var command = new RefrescarTokenCommand(correoElectronico);
        var result = await _sender.Send(command,cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }
}
