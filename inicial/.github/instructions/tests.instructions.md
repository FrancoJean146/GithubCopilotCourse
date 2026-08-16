---
description: "Convenciones de pruebas unitarias con xUnit para el catálogo de productos."
applyTo: "tests/**/*.cs"
---

# Convenciones de pruebas

- Framework: **xUnit**. Aserciones con **FluentAssertions**. Dobles de prueba con **Moq**
  o con implementaciones falsas escritas a mano cuando sean más legibles.
- Nombre del método de prueba: **`Metodo_Escenario_ResultadoEsperado`**
  (por ejemplo `CalcularPrecioFinal_CantidadDiez_AplicaCincoPorCiento`).
- Estructura **Arrange / Act / Assert** con los tres comentarios visibles:

```csharp
[Fact]
public void CalcularPrecioFinal_CantidadUno_NoAplicaDescuento()
{
    // Arrange
    var calculadora = new DescuentoCalculator();

    // Act
    var resultado = calculadora.CalcularPrecioFinal(100m, 1, false);

    // Assert
    resultado.Should().Be(100m);
}
```

- **Un assert lógico por prueba.** Si necesitas comprobar varias propiedades del mismo objeto,
  usa una sola aserción equivalente o divide la prueba.
- **Sin lógica condicional** (`if`, `switch`, `for`, `try/catch`) dentro de las pruebas.
- Casos repetitivos con `[Theory]` + `[InlineData]`; los límites exactos se prueban siempre
  en pares (valor anterior al umbral y valor del umbral).
- Cada prueba es independiente: crea su propio estado, no comparte campos mutables entre pruebas.
- Las excepciones se verifican con `Should().ThrowExactly<T>()` o `Should().ThrowAsync<T>()`.
- Prohibido `Thread.Sleep`, dependencias de red, de reloj real o de sistema de archivos.
- No modifiques ni elimines pruebas existentes para que la suite pase.
