namespace Catalogo.Api.Services;

// TODO-03: agregar documentacion XML (///) a esta interfaz y a todos sus miembros publicos.
public interface IDescuentoCalculator
{
    decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente);
}
