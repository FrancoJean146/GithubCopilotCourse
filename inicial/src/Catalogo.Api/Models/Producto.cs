namespace Catalogo.Api.Models;

/// <summary>
/// Producto del catalogo. Entidad de dominio mutable que administra el repositorio.
/// </summary>
public sealed class Producto
{
    /// <summary>Identificador unico del producto.</summary>
    public int Id { get; set; }

    /// <summary>Nombre comercial del producto.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Categoria a la que pertenece el producto.</summary>
    public Categoria Categoria { get; set; }

    /// <summary>Precio unitario de lista, sin descuentos, en la moneda del catalogo.</summary>
    public decimal PrecioBase { get; set; }

    /// <summary>Unidades disponibles en inventario.</summary>
    public int Existencias { get; set; }

    /// <summary>Indica si el producto se publica en el catalogo.</summary>
    public bool Activo { get; set; } = true;
}
