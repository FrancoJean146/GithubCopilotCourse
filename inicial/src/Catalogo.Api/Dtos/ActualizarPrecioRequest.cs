namespace Catalogo.Api.Dtos;

/// <summary>
/// Datos necesarios para recalcular y persistir el precio de un producto.
/// </summary>
public sealed record ActualizarPrecioRequest
{
    /// <summary>Precio unitario de lista sobre el que se calculan los descuentos.</summary>
    public decimal PrecioBase { get; init; }

    /// <summary>Cantidad de unidades de la operacion.</summary>
    public int Cantidad { get; init; }

    /// <summary>Indica si la operacion corresponde a un cliente preferente.</summary>
    public bool EsClientePreferente { get; init; }
}
