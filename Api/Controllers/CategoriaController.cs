using Application.Categorias.ActualizarCategoria;
using Application.Categorias.CrearCategoria;
using Application.Categorias.ObtenerCategorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriaController : Controller
{
    private readonly ISender _sender;

    public CategoriaController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Agregar")]
    public async Task<IActionResult> CrearCategoria([FromBody] CrearCategoriaRequest crearCategoriaRequest, CancellationToken cancellationToken)
    {
        var command = new CrearCategoriaCommand(crearCategoriaRequest.Nombre, crearCategoriaRequest.UserId, crearCategoriaRequest.TipoCategoria);
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPut("Actualizar")]
    public async Task<IActionResult> ActualizarCategoria([FromBody] ActualizarCategoriaRequest actualizarCategoriaRequest, CancellationToken cancellationToken)
    {
        var command = new ActualizarCategoriaCommand(actualizarCategoriaRequest.CategoriaId, actualizarCategoriaRequest.Nombre, actualizarCategoriaRequest.UserId, actualizarCategoriaRequest.TipoCategoria);
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategorias(
      Guid id,
      CancellationToken cancellationToken
  )
    {
        var query = new CategoriasQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    }

}
