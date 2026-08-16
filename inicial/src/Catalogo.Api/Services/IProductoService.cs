using Catalogo.Api.Dtos;

namespace Catalogo.Api.Services;

// TODO-03: agregar documentacion XML (///) a esta interfaz y a todos sus miembros publicos.
public interface IProductoService
{
    Task<IReadOnlyList<ProductoResponse>> BuscarAsync(BusquedaProductosQuery consulta, CancellationToken cancellationToken = default);

    Task<ProductoResponse?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task<ProductoResponse> CrearAsync(CrearProductoRequest solicitud, CancellationToken cancellationToken = default);

    Task<ProductoResponse?> ActualizarPrecioAsync(int id, ActualizarPrecioRequest solicitud, CancellationToken cancellationToken = default);

    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
