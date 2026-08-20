using System.ComponentModel.DataAnnotations;

namespace Catalogo.Api.Dtos;

/// <summary>
/// Datos necesarios para recalcular y persistir el precio de un producto.
/// </summary>
public sealed record ActualizarPrecioRequest
{
    /// <summary>Precio unitario de lista sobre el que se calculan los descuentos.</summary>
    [Range(
        typeof(decimal),
        "0.0000000000000000000000000001",
        "79228162514264337593543950335",
        ErrorMessage = "El precio base debe ser mayor que cero.",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true)]
    public decimal PrecioBase { get; init; }

    /// <summary>Cantidad de unidades de la operacion.</summary>
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual que 1.")]
    public int Cantidad { get; init; }

    /// <summary>Indica si la operacion corresponde a un cliente preferente.</summary>
    public bool EsClientePreferente { get; init; }
}
