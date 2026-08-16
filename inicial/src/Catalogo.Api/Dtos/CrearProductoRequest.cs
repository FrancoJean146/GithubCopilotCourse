using Catalogo.Api.Models;

namespace Catalogo.Api.Dtos;

/// <summary>
/// Datos necesarios para crear un producto en el catalogo.
/// </summary>
public sealed record CrearProductoRequest
{
    /// <summary>Nombre comercial del producto.</summary>
    public string Nombre { get; init; } = string.Empty;

    /// <summary>Categoria a la que pertenece el producto.</summary>
    public Categoria Categoria { get; init; }

    /// <summary>Precio unitario de lista del producto.</summary>
    public decimal PrecioBase { get; init; }

    /// <summary>Unidades disponibles en inventario.</summary>
    public int Existencias { get; init; }
}
