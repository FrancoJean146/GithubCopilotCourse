using Catalogo.Api.Models;

namespace Catalogo.Api.Repositories;

// TODO-03: agregar documentacion XML (///) a esta interfaz y a todos sus miembros publicos.
public interface IProductoRepository
{
    Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Producto> AgregarAsync(Producto producto, CancellationToken cancellationToken = default);

    Task<Producto?> ActualizarAsync(Producto producto, CancellationToken cancellationToken = default);

    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
