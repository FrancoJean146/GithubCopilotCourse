namespace Catalogo.Api.Services;

/// <summary>
/// Implementacion de las reglas de descuento del catalogo.
/// </summary>
public sealed class DescuentoCalculator : IDescuentoCalculator
{
    private const decimal DescuentoVolumenBajo = 0.05m;
    private const decimal DescuentoVolumenMedio = 0.10m;
    private const decimal DescuentoVolumenAlto = 0.15m;
    private const decimal DescuentoClientePreferente = 0.08m;
    private const int ValorTope = 100;
    private const int ValorMedio = 50;
    private const int ValorBajo= 10;

    /// <summary>
    /// Calcula el precio unitario final aplicando el descuento por volumen y el de cliente preferente.
    /// </summary>
    /// <param name="precioBase">Precio unitario de lista.</param>
    /// <param name="cantidad">Cantidad de unidades de la operacion.</param>
    /// <param name="esClientePreferente">Indica si el cliente es preferente.</param>
    /// <returns>El precio unitario final redondeado a dos decimales.</returns>
    public decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente)
    {
        if (precioBase <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(precioBase), precioBase, "El precio base debe ser mayor que cero.");
        }

        if (cantidad < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(cantidad), cantidad, "La cantidad debe ser mayor o igual que 1.");
        }

        var descuentoVolumen = ObtenerDescuentoPorVolumen(cantidad);
        var descuentoCliente = esClientePreferente ? DescuentoClientePreferente : 0m;
        var descuentoTotal = Math.Min(descuentoVolumen + descuentoCliente, 0.20m);

        var precioFinal = precioBase * (1m - descuentoTotal);

        return Math.Round(precioFinal, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal ObtenerDescuentoPorVolumen(int cantidad)
    {
        if (cantidad >= ValorTope)
        {
            return DescuentoVolumenAlto;
        }

        if (cantidad >= ValorMedio)
        {
            return DescuentoVolumenMedio;
        }

        if (cantidad >= ValorBajo)
        {
            return DescuentoVolumenBajo;
        }

        return 0m;
    }
}
