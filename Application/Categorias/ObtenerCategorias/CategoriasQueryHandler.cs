using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Abstractions;
using Domain.Usuarios.ObjectValues;

namespace Application.Categorias.ObtenerCategorias;

internal sealed class CategoriasQueryHandler : IQueryHandler<CategoriasQuery, IReadOnlyList<CategoriaResponse>>
{
    private readonly ISqlConnectionFactory _sqlConectionFactory;

    public CategoriasQueryHandler(ISqlConnectionFactory sqlConectionFactory)
    {
        _sqlConectionFactory = sqlConectionFactory;
    }

    public async Task<Result<IReadOnlyList<CategoriaResponse>>> Handle(CategoriasQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConectionFactory.CreateConnection();
        const string sql = """
               SELECT
                a.Id as Id,
                a.Nombre as Nombre,
                a.Tipo as Tipo,
                a.UsuarioId as UsuarioId
             FROM Categorias AS a
             WHERE a.UsuarioId = @UsuarioIdP    
        """;

        var categorias = await connection.QueryAsync<CategoriaResponse>(sql, new { UsuarioIdP = request.usuarioId });
        return categorias.ToList();
    }
}
