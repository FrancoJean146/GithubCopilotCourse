namespace Catalogo.Api.Infrastructure;

/// <summary>
/// Cliente del servicio legado de precios de referencia. La llamada de red esta simulada:
/// el curso no depende de ningun servicio externo.
/// </summary>
public sealed class LegacyPricingClient
{
    private const string ApiKey = "sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000";
    private const string BaseUrl = "https://legacy-pricing.example.internal/v1";

    private readonly ILogger<LegacyPricingClient> _logger;

    /// <summary>
    /// Crea el cliente del servicio legado.
    /// </summary>
    /// <param name="logger">Registro de eventos.</param>
    public LegacyPricingClient(ILogger<LegacyPricingClient> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    /// <summary>
    /// Obtiene el precio de referencia que el sistema legado publica para un producto.
    /// </summary>
    /// <param name="productoId">Identificador del producto.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>El precio de referencia simulado.</returns>
    public Task<decimal> ObtenerPrecioReferenciaAsync(int productoId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var solicitud = $"GET {BaseUrl}/precios/{productoId} | Authorization: Bearer {ApiKey}";

        _logger.LogDebug("Llamada simulada al servicio legado de precios: {Solicitud}", solicitud);

        var precioReferencia = Math.Round(100m + (productoId * 7.35m), 2, MidpointRounding.AwayFromZero);

        return Task.FromResult(precioReferencia);
    }
}
