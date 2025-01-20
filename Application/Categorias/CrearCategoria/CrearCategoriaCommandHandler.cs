using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Categorias;
using Domain.Categorias.ObjectValues;
using Domain.Categorias.Repository;
using Domain.Usuarios.ObjectValues;

namespace Application.Categorias.CrearCategoria;

public sealed class CrearCategoriaCommandHandler : ICommandHandler<CrearCategoriaCommand>
{
    private readonly ICategoriaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearCategoriaCommandHandler(ICategoriaRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CrearCategoriaCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ObtenerCategoriaSegunNombreYIdUsuario(new NombreCategoria(request.Nombre),new UsuarioId(request.UserId),(TipoCategoria)request.TipoCategoria, cancellationToken);
        if (result != null) return Result.Failure(CategoriaErrores.CategoriaExistente);
        TipoCategoria tipo = (TipoCategoria)request.TipoCategoria;
        var categoria = Categoria.Crear(new NombreCategoria(request.Nombre),new UsuarioId(request.UserId),tipo);
        _repository.Agregar(categoria);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
