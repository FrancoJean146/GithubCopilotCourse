# Análisis de `CalcularPrecioFinal`

## Funcionamiento actual

El método `CalcularPrecioFinal` de `DescuentoCalculator` calcula el precio unitario final de un producto aplicando descuentos por volumen y, opcionalmente, un descuento adicional para clientes preferentes.

Recibe tres parámetros:

- `precioBase`: precio unitario original.
- `cantidad`: número de unidades de la operación.
- `esClientePreferente`: indica si el cliente tiene la condición de preferente.

La interfaz `IDescuentoCalculator` únicamente define este contrato:

```csharp
decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente);
```

## Validación de parámetros

Antes de calcular el descuento, el método valida los argumentos:

- Si `precioBase` es menor o igual que cero, lanza `ArgumentOutOfRangeException` indicando que el precio base debe ser mayor que cero.
- Si `cantidad` es menor que uno, lanza `ArgumentOutOfRangeException` indicando que la cantidad debe ser mayor o igual que uno.

Por tanto, no se aceptan precios cero o negativos ni cantidades cero o negativas.

## Descuento por volumen

El descuento por volumen se obtiene mediante `ObtenerDescuentoPorVolumen`. Las reglas actuales son:

| Cantidad | Descuento |
|---:|---:|
| De 1 a 10 | 0% |
| De 11 a 50 | 5% |
| De 51 a 100 | 10% |
| Más de 100 | 15% |

Los límites se comprueban con comparaciones estrictas (`>`):

```csharp
if (cantidad > 100)
{
    return 0.15m;
}

if (cantidad > 50)
{
    return 0.10m;
}

if (cantidad > 10)
{
    return 0.05m;
}

return 0m;
```

Esto significa que, por ejemplo, una cantidad de 10 no obtiene descuento por volumen, mientras que una cantidad de 11 obtiene un 5%.

## Descuento para clientes preferentes

Cuando `esClientePreferente` es `true`, se suma un descuento adicional del 8%:

```csharp
if (esClientePreferente)
{
    descuento += 0.08m;
}
```

El descuento preferente se suma al descuento por volumen. No se aplica de forma sucesiva sobre un precio ya rebajado.

## Cálculo del precio final

El precio final se calcula con la fórmula:

```csharp
var precioFinal = precioBase * (1m - descuento);
```

Después, el resultado se redondea a dos decimales usando `MidpointRounding.AwayFromZero`:

```csharp
return Math.Round(precioFinal, 2, MidpointRounding.AwayFromZero);
```

El método devuelve el precio unitario final, no el importe total de todas las unidades.

## Ejemplos

### Sin descuento por volumen

```csharp
CalcularPrecioFinal(100m, 5, false)
```

- Descuento por volumen: 0%.
- Descuento preferente: 0%.
- Resultado: `100.00`.

### Descuento bajo por volumen

```csharp
CalcularPrecioFinal(100m, 11, false)
```

- Descuento por volumen: 5%.
- Resultado: `95.00`.

### Descuento medio por volumen

```csharp
CalcularPrecioFinal(100m, 51, false)
```

- Descuento por volumen: 10%.
- Resultado: `90.00`.

### Cliente preferente sin descuento por volumen

```csharp
CalcularPrecioFinal(100m, 5, true)
```

- Descuento por volumen: 0%.
- Descuento preferente: 8%.
- Descuento total: 8%.
- Resultado: `92.00`.

### Cliente preferente con descuento por volumen

```csharp
CalcularPrecioFinal(100m, 11, true)
```

- Descuento por volumen: 5%.
- Descuento preferente: 8%.
- Descuento total: 13%.
- Resultado: `87.00`.

### Cantidad alta y cliente preferente

```csharp
CalcularPrecioFinal(100m, 101, true)
```

- Descuento por volumen: 15%.
- Descuento preferente: 8%.
- Descuento total: 23%.
- Resultado: `77.00`.

### Redondeo a dos decimales

```csharp
CalcularPrecioFinal(99.99m, 11, false)
```

El cálculo produce `94.9905` antes del redondeo. El resultado devuelto es `94.99`.

## Situación relevante no implementada

En las pruebas existe un caso omitido que espera limitar el descuento total al 20% cuando se combinan descuentos de volumen y de cliente preferente.

Actualmente no se aplica ese límite. Por ejemplo:

```csharp
CalcularPrecioFinal(100m, 101, true)
```

devuelve `77.00`, ya que suma un 15% de descuento por volumen y un 8% de descuento preferente, para un total del 23%.

Por ello, el comportamiento actual permite descuentos totales del 18% para cantidades entre 51 y 100 con cliente preferente, y del 23% para cantidades superiores a 100 con cliente preferente.
