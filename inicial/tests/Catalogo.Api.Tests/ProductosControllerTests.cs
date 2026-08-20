using System.ComponentModel.DataAnnotations;
using Catalogo.Api.Controllers;
using Catalogo.Api.Dtos;
using Catalogo.Api.Models;
using Catalogo.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalogo.Api.Tests;

/// <summary>
/// Pruebas del contrato HTTP para la actualizacion del precio de productos.
/// </summary>
public sealed class ProductosControllerTests
{
    [Fact]
    public async Task ActualizarPrecioAsync_ProductoExistente_DevuelveOkConProductoActualizado()
    {
        // Arrange
        var solicitud = CrearSolicitudValida();
        var producto = CrearProductoResponse();
        var servicio = new Mock<IProductoService>();
        servicio
            .Setup(actual => actual.ActualizarPrecioAsync(1, solicitud, It.IsAny<CancellationToken>()))
            .ReturnsAsync(producto);
        var controller = new ProductosController(servicio.Object);

        // Act
        var resultado = await controller.ActualizarPrecioAsync(1, solicitud, default);

        // Assert
        resultado.Result.Should().BeEquivalentTo(new OkObjectResult(producto));
    }

    [Fact]
    public async Task ActualizarPrecioAsync_ProductoInexistente_DevuelveProblemDetailsNotFound()
    {
        // Arrange
        var solicitud = CrearSolicitudValida();
        var servicio = new Mock<IProductoService>();
        servicio
            .Setup(actual => actual.ActualizarPrecioAsync(9999, solicitud, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductoResponse?)null);
        var controller = new ProductosController(servicio.Object);
        var problemaEsperado = new ProblemDetails
        {
            Title = "Producto no encontrado",
            Detail = "No existe un producto con el identificador 9999.",
            Status = StatusCodes.Status404NotFound
        };

        // Act
        var resultado = await controller.ActualizarPrecioAsync(9999, solicitud, default);

        // Assert
        resultado.Result.Should().BeEquivalentTo(new NotFoundObjectResult(problemaEsperado));
    }

    [Theory]
    [InlineData(0.00, 1, "PrecioBase")]
    [InlineData(100.00, 0, "Cantidad")]
    public async Task ActualizarPrecioAsync_SolicitudInvalida_DevuelveProblemDetailsConCampoInvalido(
        double precioBase,
        int cantidad,
        string campoEsperado)
    {
        // Arrange
        var solicitud = new ActualizarPrecioRequest
        {
            PrecioBase = (decimal)precioBase,
            Cantidad = cantidad,
            EsClientePreferente = false
        };
        var excepcion = new ValidationException(
            new ValidationResult("Valor fuera de rango.", new[] { campoEsperado }),
            null,
            precioBase);
        var servicio = new Mock<IProductoService>();
        servicio
            .Setup(actual => actual.ActualizarPrecioAsync(1, solicitud, It.IsAny<CancellationToken>()))
            .ThrowsAsync(excepcion);
        var controller = new ProductosController(servicio.Object);
        var problemaEsperado = new ProblemDetails
        {
            Title = "Datos invalidos",
            Detail = excepcion.Message,
            Status = StatusCodes.Status400BadRequest,
            Extensions = { ["invalidField"] = campoEsperado }
        };

        // Act
        var resultado = await controller.ActualizarPrecioAsync(1, solicitud, default);

        // Assert
        resultado.Result.Should().BeEquivalentTo(new BadRequestObjectResult(problemaEsperado));
    }

    private static ActualizarPrecioRequest CrearSolicitudValida()
    {
        return new ActualizarPrecioRequest
        {
            PrecioBase = 100m,
            Cantidad = 10,
            EsClientePreferente = false
        };
    }

    private static ProductoResponse CrearProductoResponse()
    {
        return new ProductoResponse(
            1,
            "Monitor curvo 27 pulgadas",
            Categoria.Electronica,
            95m,
            14,
            true);
    }
}
