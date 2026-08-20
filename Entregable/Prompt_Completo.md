# Análisis del comportamiento real de CalcularPrecioFinal

## 1. Tabla de comportamiento

| afirmación | archivo:línea | cómo lo compruebo |
| ---------- | ------------- | ----------------- |
| Validación: si `precioBase <= 0m`, el método lanza `ArgumentOutOfRangeException`. | DescuentoCalculator.cs:22-25 | La condición `if (precioBase <= 0m)` ejecuta `throw new ArgumentOutOfRangeException(nameof(precioBase), precioBase, "El precio base debe ser mayor que cero.");`. |
| Validación: si `cantidad < 1`, el método lanza `ArgumentOutOfRangeException`. | DescuentoCalculator.cs:27-30 | La condición `if (cantidad < 1)` ejecuta `throw new ArgumentOutOfRangeException(nameof(cantidad), cantidad, "La cantidad debe ser mayor o igual que 1.");`. |
| Cuando una validación falla, la ejecución del método se interrumpe con la excepción y no continúa con los cálculos. | DescuentoCalculator.cs:22-30 | El `throw` está dentro de cada `if`; el flujo no alcanza la siguiente instrucción del método después de la validación fallida. |
| Cálculo: el descuento por volumen se obtiene con `ObtenerDescuentoPorVolumen(cantidad)`. | DescuentoCalculator.cs:32, 44-61 | `var descuento = ObtenerDescuentoPorVolumen(cantidad);` y ese método devuelve `DescuentoVolumenAlto` (`0.15m`), `DescuentoVolumenMedio` (`0.10m`), `DescuentoVolumenBajo` (`0.05m`) o `0m` según la cantidad. |
| Cálculo: cuando `esClientePreferente` es `true`, se suma `DescuentoClientePreferente` al descuento actual. | DescuentoCalculator.cs:34-36 | `if (esClientePreferente) { descuento += DescuentoClientePreferente; }` y `DescuentoClientePreferente` vale `0.08m`. |
| Cálculo: cuando `esClientePreferente` es `false`, no se añade ningún descuento adicional. | DescuentoCalculator.cs:34-37 | La condición `if (esClientePreferente)` no ejecuta la suma si el valor es `false`. |
| Fórmula usada para calcular `precioFinal`: `precioBase * (1m - descuento)`. | DescuentoCalculator.cs:39 | La variable se asigna con `var precioFinal = precioBase * (1m - descuento);`. |
| Redondeo: el resultado se redondea antes de devolverlo, con dos decimales y `MidpointRounding.AwayFromZero`. | DescuentoCalculator.cs:41 | La instrucción final es `return Math.Round(precioFinal, 2, MidpointRounding.AwayFromZero);`. |
| Relación observable: `DescuentoCalculator` implementa `IDescuentoCalculator`. | DescuentoCalculator.cs:6; IDescuentoCalculator.cs:4-6 | La clase declara `: IDescuentoCalculator` y la interfaz declara `decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente);`. |

## 2. Tabla de casos

| precioBase | cantidad | esClientePreferente | resultado o excepción | líneas que lo determinan |
| ---------: | -------: | ------------------- | --------------------- | ------------------------ |
| 0 | 1 | false | `throw new ArgumentOutOfRangeException(nameof(precioBase), precioBase, "El precio base debe ser mayor que cero.");` | DescuentoCalculator.cs:22-25 |
| -1 | 10 | true | `throw new ArgumentOutOfRangeException(nameof(precioBase), precioBase, "El precio base debe ser mayor que cero.");` | DescuentoCalculator.cs:22-25 |
| 100 | 1 | false | `100` | DescuentoCalculator.cs:27-30, 32, 44-61, 34-41 |
| 100 | 11 | false | `95` | DescuentoCalculator.cs:27-30, 32, 44-61, 34-41 |
| 100 | 11 | true | `87` | DescuentoCalculator.cs:27-30, 32, 44-61, 34-41 |
| 19.99 | 51 | true | `16.39` | DescuentoCalculator.cs:27-30, 32, 44-61, 34-41 |

### Desglose de los casos con operaciones intermedias

- Caso `100, 1, false`: `cantidad = 1` entra en `ObtenerDescuentoPorVolumen`, que devuelve `0m` porque ninguna condición `> 100`, `> 50` ni `> 10` se cumple; `esClientePreferente` es `false`; `precioFinal = 100 * (1m - 0m) = 100`; `Math.Round(100, 2, MidpointRounding.AwayFromZero) = 100`. Determinante: DescuentoCalculator.cs:27-30, 32, 44-61, 39, 41.
- Caso `100, 11, false`: `cantidad = 11` hace que `ObtenerDescuentoPorVolumen` devuelva `0.05m`; `esClientePreferente` es `false`; `precioFinal = 100 * (1m - 0.05m) = 95m`; `Math.Round(95, 2, MidpointRounding.AwayFromZero) = 95`. Determinante: DescuentoCalculator.cs:27-30, 32, 44-61, 39, 41.
- Caso `100, 11, true`: `cantidad = 11` hace que `ObtenerDescuentoPorVolumen` devuelva `0.05m`; `esClientePreferente` añade `0.08m`; `descuento = 0.05m + 0.08m = 0.13m`; `precioFinal = 100 * (1m - 0.13m) = 87m`; `Math.Round(87, 2, MidpointRounding.AwayFromZero) = 87`. Determinante: DescuentoCalculator.cs:27-30, 32, 34-41, 44-61.
- Caso `19.99, 51, true`: `cantidad = 51` hace que `ObtenerDescuentoPorVolumen` devuelva `0.10m`; `esClientePreferente` añade `0.08m`; `descuento = 0.10m + 0.08m = 0.18m`; `precioFinal = 19.99 * (1m - 0.18m) = 16.3918m`; `Math.Round(16.3918m, 2, MidpointRounding.AwayFromZero) = 16.39`. Determinante: DescuentoCalculator.cs:27-30, 32, 34-41, 44-61.

## 3. Orden real de ejecución

1. Se entra en `CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente)`. Determinante: DescuentoCalculator.cs:20.
2. Se ejecuta la validación `if (precioBase <= 0m)`. Si se cumple, se lanza `ArgumentOutOfRangeException` y la ejecución del método termina. Determinante: DescuentoCalculator.cs:22-25.
3. Si la validación anterior no falla, se ejecuta la validación `if (cantidad < 1)`. Si se cumple, se lanza `ArgumentOutOfRangeException` y la ejecución del método termina. Determinante: DescuentoCalculator.cs:27-30.
4. Si ambas validaciones pasan, se ejecuta `var descuento = ObtenerDescuentoPorVolumen(cantidad);`. Determinante: DescuentoCalculator.cs:32.
5. Se ejecuta `ObtenerDescuentoPorVolumen(int cantidad)`. La secuencia es: si `cantidad > 100`, retorna `DescuentoVolumenAlto` (`0.15m`); si no, si `cantidad > 50`, retorna `DescuentoVolumenMedio` (`0.10m`); si no, si `cantidad > 10`, retorna `DescuentoVolumenBajo` (`0.05m`); en cualquier otro caso, retorna `0m`. Determinante: DescuentoCalculator.cs:44-61.
6. Se evalúa `if (esClientePreferente)`. Si es `true`, ejecuta `descuento += DescuentoClientePreferente;` con `DescuentoClientePreferente = 0.08m`. Si es `false`, no suma nada. Determinante: DescuentoCalculator.cs:34-37.
7. Se calcula `var precioFinal = precioBase * (1m - descuento);`. Determinante: DescuentoCalculator.cs:39.
8. Se devuelve `Math.Round(precioFinal, 2, MidpointRounding.AwayFromZero);`. El redondeo ocurre exactamente aquí, justo antes de devolver el resultado. Determinante: DescuentoCalculator.cs:41.

## 4. Lista: "Lo que este código no permite afirmar"

- No verificable con el contexto adjunto
- No verificable con el contexto adjunto
- No verificable con el contexto adjunto
