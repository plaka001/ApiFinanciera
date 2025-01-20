using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Categorias;
using Domain.Categorias.ObjectValues;
using Domain.Categorias.Repository;
using Domain.Usuarios.ObjectValues;

namespace Application.Categorias.ActualizarCategoria;

public sealed class ActualizarCategoriaCommandHandler : ICommandHandler<ActualizarCategoriaCommand>
{
    private readonly ICategoriaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarCategoriaCommandHandler(ICategoriaRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _repository.ObtenerSegunId(new CategoriaId(request.CategoriaId));
        if (categoria == null) return Result.Failure(CategoriaErrores.NoEncontrada);
        var categoriaNew = Categoria.Actualizar(categoria, new NombreCategoria(request.Nombre), (TipoCategoria)request.TipoCategoria);
        _repository.Actualizar(categoriaNew);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
