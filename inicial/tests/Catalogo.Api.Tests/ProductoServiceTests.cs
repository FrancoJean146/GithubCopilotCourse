using Catalogo.Api.Dtos;
using Catalogo.Api.Models;
using Catalogo.Api.Repositories;
using Catalogo.Api.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Catalogo.Api.Tests;

/// <summary>
/// Pruebas del servicio de catalogo de productos.
/// </summary>
public sealed class ProductoServiceTests
{
    private static ProductoService CrearServicio()
    {
        return new ProductoService(
            new InMemoryProductoRepository(),
            new DescuentoCalculator(),
            NullLogger<ProductoService>.Instance);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_ProductoExistente_DevuelveElProductoSolicitado()
    {
        // Arrange
        var servicio = CrearServicio();

        // Act
        var producto = await servicio.ObtenerPorIdAsync(1);

        // Assert
        producto!.Nombre.Should().Be("Monitor curvo 27 pulgadas");
    }

    [Fact]
    public async Task ObtenerPorIdAsync_ProductoInexistente_DevuelveNull()
    {
        // Arrange
        var servicio = CrearServicio();

        // Act
        var producto = await servicio.ObtenerPorIdAsync(9999);

        // Assert
        producto.Should().BeNull();
    }

    [Fact]
    public async Task CrearAsync_SolicitudValida_AsignaElSiguienteIdentificador()
    {
        // Arrange
        var servicio = CrearServicio();
        var solicitud = new CrearProductoRequest
        {
            Nombre = "Alfombrilla de escritorio",
            Categoria = Categoria.Oficina,
            PrecioBase = 39.90m,
            Existencias = 25
        };

        // Act
        var creado = await servicio.CrearAsync(solicitud);

        // Assert
        creado.Id.Should().Be(13);
    }

    [Fact]
    public async Task EliminarAsync_ProductoExistente_DevuelveTrue()
    {
        // Arrange
        var servicio = CrearServicio();

        // Act
        var eliminado = await servicio.EliminarAsync(12);

        // Assert
        eliminado.Should().BeTrue();
    }

    [Fact(Skip = "Habilitar en el Laboratorio 5 (Jornada 3) al implementar HU-01")]
    public async Task ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado()
    {
        // Arrange
        var servicio = CrearServicio();
        var solicitud = new ActualizarPrecioRequest
        {
            PrecioBase = 100m,
            Cantidad = 11,
            EsClientePreferente = false
        };

        // Act
        var actualizado = await servicio.ActualizarPrecioAsync(1, solicitud);

        // Assert
        actualizado!.PrecioBase.Should().Be(95.00m);
    }
}
