namespace Catalogo.Api.Models;

/// <summary>
/// Categorias disponibles para clasificar los productos del catalogo.
/// </summary>
public enum Categoria
{
    /// <summary>Dispositivos y accesorios electronicos.</summary>
    Electronica = 0,

    /// <summary>Articulos para el hogar.</summary>
    Hogar = 1,

    /// <summary>Mobiliario y articulos de oficina.</summary>
    Oficina = 2,

    /// <summary>Insumos de consumo periodico.</summary>
    Consumible = 3
}
