using Catalogo.Api.Dtos;
using Catalogo.Api.Models;
using Catalogo.Api.Repositories;

namespace Catalogo.Api.Services;

/// <summary>
/// Reglas de negocio del catalogo de productos.
/// </summary>
public sealed class ProductoService : IProductoService
{
    private readonly IProductoRepository _repositorio;
    private readonly IDescuentoCalculator _calculadora;
    private readonly ILogger<ProductoService> _logger;

    /// <summary>
    /// Crea el servicio con sus dependencias.
    /// </summary>
    /// <param name="repositorio">Repositorio de productos.</param>
    /// <param name="calculadora">Calculadora de descuentos.</param>
    /// <param name="logger">Registro de eventos.</param>
    public ProductoService(
        IProductoRepository repositorio,
        IDescuentoCalculator calculadora,
        ILogger<ProductoService> logger)
    {
        ArgumentNullException.ThrowIfNull(repositorio);
        ArgumentNullException.ThrowIfNull(calculadora);
        ArgumentNullException.ThrowIfNull(logger);

        _repositorio = repositorio;
        _calculadora = calculadora;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<ProductoResponse>> BuscarAsync(BusquedaProductosQuery consulta, CancellationToken cancellationToken = default)
    {
        // TODO-02: filtrar por categoria, rango de precio y texto del nombre, y paginar el resultado.
        throw new NotImplementedException("TODO-02: la busqueda de productos todavia no esta implementada.");
    }

    /// <inheritdoc />
    public async Task<ProductoResponse?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var producto = await _repositorio.ObtenerPorIdAsync(id, cancellationToken).ConfigureAwait(false);

        return producto is null ? null : Mapear(producto);
    }

    /// <inheritdoc />
    public async Task<ProductoResponse> CrearAsync(CrearProductoRequest solicitud, CancellationToken cancellationToken = default)
    {
        var producto = new Producto
        {
            Nombre = solicitud.Nombre,
            Categoria = solicitud.Categoria,
            PrecioBase = solicitud.PrecioBase,
            Existencias = solicitud.Existencias,
            Activo = true
        };

        var creado = await _repositorio.AgregarAsync(producto, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Producto {ProductoId} creado en el catalogo.", creado.Id);

        return Mapear(creado);
    }

    /// <inheritdoc />
    public Task<ProductoResponse?> ActualizarPrecioAsync(int id, ActualizarPrecioRequest solicitud, CancellationToken cancellationToken = default)
    {
        // TODO-01: calcular el precio final con IDescuentoCalculator, persistirlo y devolver el ProductoResponse.
        throw new NotImplementedException("TODO-01: la actualizacion de precio todavia no esta implementada.");
    }

    /// <inheritdoc />
    public async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        var eliminado = await _repositorio.EliminarAsync(id, cancellationToken).ConfigureAwait(false);

        if (eliminado)
        {
            _logger.LogInformation("Producto {ProductoId} eliminado del catalogo.", id);
        }

        return eliminado;
    }

    private static ProductoResponse Mapear(Producto producto)
    {
        return new ProductoResponse(
            producto.Id,
            producto.Nombre,
            producto.Categoria,
            producto.PrecioBase,
            producto.Existencias,
            producto.Activo);
    }
}
