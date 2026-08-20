using Catalogo.Api.Services;
using FluentAssertions;
using Xunit;

namespace Catalogo.Api.Tests;

/// <summary>
/// Pruebas de las reglas de descuento del catalogo.
/// </summary>
public sealed class DescuentoCalculatorTests
{
    [Theory]
    [InlineData(100.00, 1)]
    [InlineData(100.00, 9)]
    public void CalcularPrecioFinal_CantidadSinVolumen_DevuelveElPrecioBase(double precioBase, int cantidad)
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal((decimal)precioBase, cantidad, false);

        // Assert
        resultado.Should().Be((decimal)precioBase);
    }

    [Theory]
    [InlineData(100.00, 10, 95.00)]
    [InlineData(100.00, 11, 95.00)]
    [InlineData(100.00, 49, 95.00)]
    [InlineData(100.00, 50, 90.00)]
    [InlineData(100.00, 51, 90.00)]
    [InlineData(100.00, 99, 90.00)]
    [InlineData(100.00, 100, 85.00)]
    [InlineData(100.00, 101, 85.00)]
    [InlineData(99.99, 11, 94.99)]
    public void CalcularPrecioFinal_CantidadConVolumen_AplicaElDescuentoPorVolumen(double precioBase, int cantidad, double esperado)
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal((decimal)precioBase, cantidad, false);

        // Assert
        resultado.Should().Be((decimal)esperado);
    }

    [Fact]
    public void CalcularPrecioFinal_ClientePreferenteSinVolumen_AplicaOchoPorCiento()
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal(100m, 5, true);

        // Assert
        resultado.Should().Be(92.00m);
    }

    [Fact]
    public void CalcularPrecioFinal_VolumenYClientePreferente_SumaLosDescuentos()
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal(100m, 10, true);

        // Assert
        resultado.Should().Be(87.00m);
    }

    [Fact]
    public void CalcularPrecioFinal_ResultadoConTresDecimales_RedondeaAlejandoseDeCero()
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal(10.125m, 1, false);

        // Assert
        resultado.Should().Be(10.13m);
    }

    [Theory]
    [InlineData(0.00, 1)]
    [InlineData(-1.00, 1)]
    [InlineData(100.00, 0)]
    [InlineData(100.00, -3)]
    public void CalcularPrecioFinal_ArgumentosInvalidos_LanzaArgumentOutOfRangeException(double precioBase, int cantidad)
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        Action accion = () => calculadora.CalcularPrecioFinal((decimal)precioBase, cantidad, false);

        // Assert
        accion.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento()
    {
        // Arrange
        var calculadora = new DescuentoCalculator();

        // Act
        var resultado = calculadora.CalcularPrecioFinal(100m, 100, true);

        // Assert
        resultado.Should().Be(80.00m);
    }
}
