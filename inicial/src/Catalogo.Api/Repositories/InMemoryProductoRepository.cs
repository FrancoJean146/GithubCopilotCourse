using System.Collections.Concurrent;
using Catalogo.Api.Models;

namespace Catalogo.Api.Repositories;

/// <summary>
/// Repositorio en memoria con una semilla de 12 productos. Se registra como singleton,
/// por lo que el estado se comparte entre peticiones mientras el proceso vive.
/// </summary>
public sealed class InMemoryProductoRepository : IProductoRepository
{
    private readonly ConcurrentDictionary<int, Producto> _productos = new();
    private int _ultimoId;

    /// <summary>
    /// Crea el repositorio y carga la semilla de productos de demostracion.
    /// </summary>
    public InMemoryProductoRepository()
    {
        foreach (var producto in CrearSemilla())
        {
            _productos[producto.Id] = producto;
            if (producto.Id > _ultimoId)
            {
                _ultimoId = producto.Id;
            }
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyList<Producto> resultado = _productos.Values
            .OrderBy(producto => producto.Id)
            .ToList();

        return Task.FromResult(resultado);
    }

    /// <inheritdoc />
    public Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _productos.TryGetValue(id, out var producto);
        return Task.FromResult(producto);
    }

    /// <inheritdoc />
    public Task<Producto> AgregarAsync(Producto producto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(producto);
        cancellationToken.ThrowIfCancellationRequested();

        producto.Id = Interlocked.Increment(ref _ultimoId);
        _productos[producto.Id] = producto;

        return Task.FromResult(producto);
    }

    /// <inheritdoc />
    public Task<Producto?> ActualizarAsync(Producto producto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(producto);
        cancellationToken.ThrowIfCancellationRequested();

        if (!_productos.ContainsKey(producto.Id))
        {
            return Task.FromResult<Producto?>(null);
        }

        _productos[producto.Id] = producto;
        return Task.FromResult<Producto?>(producto);
    }

    /// <inheritdoc />
    public Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_productos.TryRemove(id, out _));
    }

    private static IEnumerable<Producto> CrearSemilla()
    {
        return new[]
        {
            new Producto { Id = 1, Nombre = "Monitor curvo 27 pulgadas", Categoria = Categoria.Electronica, PrecioBase = 899.99m, Existencias = 14, Activo = true },
            new Producto { Id = 2, Nombre = "Teclado mecanico retroiluminado", Categoria = Categoria.Electronica, PrecioBase = 249.50m, Existencias = 40, Activo = true },
            new Producto { Id = 3, Nombre = "Mouse inalambrico ergonomico", Categoria = Categoria.Electronica, PrecioBase = 129.90m, Existencias = 65, Activo = true },
            new Producto { Id = 4, Nombre = "Auriculares con cancelacion de ruido", Categoria = Categoria.Electronica, PrecioBase = 549.00m, Existencias = 22, Activo = true },
            new Producto { Id = 5, Nombre = "Lampara de escritorio LED", Categoria = Categoria.Hogar, PrecioBase = 89.99m, Existencias = 80, Activo = true },
            new Producto { Id = 6, Nombre = "Cafetera de goteo programable", Categoria = Categoria.Hogar, PrecioBase = 199.00m, Existencias = 18, Activo = true },
            new Producto { Id = 7, Nombre = "Organizador modular de cocina", Categoria = Categoria.Hogar, PrecioBase = 74.50m, Existencias = 120, Activo = true },
            new Producto { Id = 8, Nombre = "Silla ergonomica de oficina", Categoria = Categoria.Oficina, PrecioBase = 1250.00m, Existencias = 9, Activo = true },
            new Producto { Id = 9, Nombre = "Escritorio elevable manual", Categoria = Categoria.Oficina, PrecioBase = 1899.90m, Existencias = 5, Activo = true },
            new Producto { Id = 10, Nombre = "Archivador metalico de tres cajones", Categoria = Categoria.Oficina, PrecioBase = 459.00m, Existencias = 11, Activo = false },
            new Producto { Id = 11, Nombre = "Resma de papel A4 500 hojas", Categoria = Categoria.Consumible, PrecioBase = 24.90m, Existencias = 300, Activo = true },
            new Producto { Id = 12, Nombre = "Cartucho de tinta negra", Categoria = Categoria.Consumible, PrecioBase = 89.50m, Existencias = 150, Activo = true }
        };
    }
}
