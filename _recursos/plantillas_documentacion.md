# Plantillas de documentación técnica

Seis plantillas listas para rellenar durante los laboratorios, escritas contra el repositorio de práctica `copilot-lab-catalogo` (`05_codigo_laboratorios/inicial/`), de modo que cada ejemplo puede comprobarse abriendo el archivo citado. Estado de referencia del repositorio inicial: compila con 0 warnings y `dotnet test` da **15 pruebas superadas y 2 omitidas**.

**Advertencia obligatoria:** los fragmentos presentados como salida de Copilot son ilustrativos. **La salida real variará** entre ejecuciones, modelos y versiones del producto. Ningún texto generado se acepta sin la verificación que cada plantilla describe.

## Convenciones aplicables a las seis plantillas

| Regla | Detalle |
|---|---|
| Idioma | Español profesional acentuado en la prosa |
| Identificadores | Sin acentos, tal como están en el código, y entre backticks: `DescuentoCalculator`, `ObtenerPorIdAsync` |
| Comandos y productos | En inglés y entre backticks: `dotnet test`, `Copilot Chat`, `ProblemDetails` |
| Marcadores | `<...>` se sustituye íntegramente, signos incluidos |
| Formato | Tablas para todo dato tabular; bloques de código con lenguaje declarado; sin emojis |
| Referencias | Ruta relativa desde la raíz del repositorio y línea aproximada cuando aplique |

---

# Plantilla 1 — Nota de módulo

**Propósito:** dejar por escrito qué hace un módulo, de qué depende, cómo fluye una llamada, qué riesgos tiene y qué se puede mejorar, en una página que otro desarrollador lea antes de tocar el código.

| Campo | Valor |
|---|---|
| Cuándo se usa | Al llegar a un módulo desconocido, antes de refactorizarlo o de estimar un cambio |
| Quién la escribe | La persona que va a modificar el módulo; se revisa en el Pull Request |
| Dónde vive | `docs/modulos/<nombre>.md` o la bitácora del participante |
| Estructura | Idéntica a la del prompt file `.github/prompts/explicar-modulo.prompt.md` |

## Plantilla

```markdown
# Nota de módulo — <ruta/del/archivo/o/carpeta>
- **Fecha:** <AAAA-MM-DD> · **Autor:** <nombre> · **Revisión:** <hash corto>

## 1. Propósito
<Una o dos frases: qué problema resuelve y quién lo usa.>

## 2. Dependencias
| Dependencia | Tipo (entrante/saliente) | Para qué se usa |
|---|---|---|
| <tipo o archivo> | <entrante o saliente> | <uso concreto> |

## 3. Flujo principal
1. <Punto de entrada: quién llama y con qué tipos.>
2. <Paso intermedio.>
3. <Retorno: qué devuelve y en qué unidad.>

## 4. Riesgos (máximo 5; cubrir validación, errores, secretos y concurrencia)
| # | Riesgo | Archivo | Línea aprox. | Severidad |
|---|---|---|---|---|
| 1 | <manifestación observable> | <ruta> | <n-m> | <Alta/Media/Baja> |

## 5. Oportunidades de mejora (máximo 5, de mayor a menor valor/esfuerzo)
| # | Cambio propuesto | Valor | Esfuerzo | Cómo se verifica |
|---|---|---|---|---|
| 1 | <cambio concreto> | <Alto/Medio/Bajo> | <Alto/Medio/Bajo> | <comando o prueba> |

## 6. No determinable con el contexto actual
- <Lo que no pudo confirmarse leyendo el código.>
```

## Ejemplo rellenado: `src/Catalogo.Api/Services/DescuentoCalculator.cs`

```markdown
# Nota de módulo — src/Catalogo.Api/Services/DescuentoCalculator.cs
- **Fecha:** 2026-08-14 · **Autor:** <participante> · **Revisión:** estado inicial de `inicial/`

## 1. Propósito
Calcula el precio unitario final de una operación aplicando el descuento por volumen y el de cliente
preferente. Lo consume `ProductoService` únicamente a través de `IDescuentoCalculator`.

## 2. Dependencias
| Dependencia | Tipo | Para qué se usa |
|---|---|---|
| `IDescuentoCalculator` | Saliente | Único contrato que expone; punto de acoplamiento del sistema |
| `ProductoService` | Entrante | Lo recibe por constructor en el campo `_calculadora` |
| `Program.cs` línea 16 | Entrante | `AddSingleton<IDescuentoCalculator, DescuentoCalculator>()` |
| `DescuentoCalculatorTests` | Entrante | Instancia la clase concreta con `new DescuentoCalculator()` |
| `System.Math` | Saliente | `Math.Round(valor, 2, MidpointRounding.AwayFromZero)` |

La clase no recibe ninguna dependencia por constructor: ni repositorio, ni configuración, ni `ILogger`.

## 3. Flujo principal
1. `ProductosController` recibe la petición y delega en `IProductoService`.
2. `ProductoService` invoca `CalcularPrecioFinal(precioBase, cantidad, esClientePreferente)`.
3. Se valida `precioBase > 0` y `cantidad >= 1`; si no, `ArgumentOutOfRangeException` (líneas 22-30).
4. `ObtenerDescuentoPorVolumen(cantidad)` devuelve 0,15, 0,10, 0,05 o 0 según el tramo (líneas 44-62).
5. Si el cliente es preferente se suma `DescuentoClientePreferente = 0.08m` (líneas 34-37).
6. Se calcula `precioBase * (1m - descuento)` y se redondea a dos decimales (líneas 39-41). El valor
   devuelto es el **precio unitario**, no el importe total.

## 4. Riesgos
| # | Riesgo | Archivo | Línea aprox. | Severidad |
|---|---|---|---|---|
| 1 | BUG-01 (A): los tres umbrales comparan con `>` en vez de `>=`, así que 10, 50 y 100 unidades caen en el tramo inferior. Con `precioBase = 100` y `cantidad = 10` devuelve 100,00 cuando la regla exige 95,00 | `Services/DescuentoCalculator.cs` | 44-62 | Alta |
| 2 | BUG-01 (B): no existe constante de tope ni comparación que limite el descuento acumulado al 20 %. Con `cantidad = 100` y cliente preferente devuelve 82,00 en vez de 80,00 | `Services/DescuentoCalculator.cs` | 32-41 | Alta |
| 3 | Manejo de errores: la `ArgumentOutOfRangeException` sale sin traducir porque el middleware de `ProblemDetails` está pendiente (TODO-03), y llega al cliente como 500 | `Program.cs` | 22-23 | Media |
| 4 | Validación: se acota `precioBase` y `cantidad`, pero no el descuento acumulado; sin tope, cualquier regla adicional puede llevar `(1m - descuento)` por debajo de cero | `Services/DescuentoCalculator.cs` | 32-39 | Media |
| 5 | Concurrencia y secretos: clase `sealed`, sin estado mutable y con cuatro campos `const`, por lo que `AddSingleton` es seguro. No contiene secretos; el secreto simulado del ejercicio está en `Infrastructure/LegacyPricingClient.cs` línea 9 | `Program.cs` | 16 | Baja |

## 5. Oportunidades de mejora
| # | Cambio propuesto | Valor | Esfuerzo | Cómo se verifica |
|---|---|---|---|---|
| 1 | `>=` en los tres umbrales y `TopeDescuento = 0.20m` aplicado tras sumar el 8 % | Alto | Bajo | Quitar el `Skip` de `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` y ejecutar `dotnet test` |
| 2 | Pruebas de umbral para 9/10, 49/50 y 99/100 | Alto | Bajo | `dotnet test --filter DescuentoCalculatorTests` |
| 3 | Documentar `IDescuentoCalculator` con XML (TODO-03) y activar `GenerateDocumentationFile` | Medio | Bajo | `dotnet build` sin CS1591 |
| 4 | Sustituir la cadena de tres `if` por una tabla de tramos ordenada | Medio | Medio | Las pruebas de umbral siguen en verde |
| 5 | Llevar los porcentajes a `IOptions<PoliticaDescuentos>` para cambiarlos sin recompilar | Medio | Alto | Prueba de integración que altere los valores por configuración |

## 6. No determinable con el contexto actual
- Si la política comercial contempla descuentos negociados además del 8 % de cliente preferente.
- Qué se espera con cantidades muy altas: el código no impone cota superior a `cantidad`.
```

## Reparto Copilot / persona y verificación

| Apartado | Copilot `[GA]` | Persona |
|---|---|---|
| 1-3 Propósito, dependencias y flujo | Borrador e inventario, incluida la línea del registro en `Program.cs` | Confirma que el retorno se describa como precio unitario y que ningún consumidor sea inventado |
| 4 Riesgos | Candidatos y ubicación | **Clasifica severidad y decide cuáles son los cinco**; el orden es juicio humano |
| 5 Mejoras | Lista de cambios posibles | Ordena por valor/esfuerzo y define la verificación |
| 6 No determinable | Marca lo que no puede confirmar | Decide a quién se pregunta |

**Cómo verificar:** abrir cada archivo citado y confirmar la línea (un desfase de dos o tres líneas es tolerable; un método inexistente invalida la nota); ejecutar `dotnet build` y `dotnet test` y comprobar el estado 15 superadas / 2 omitidas; reproducir los pares 100,00 frente a 95,00 y 82,00 frente a 80,00 con una prueba, no por lectura; eliminar toda afirmación sobre cachés, transacciones o concurrencia que el código no respalde.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| Copiar el `<summary>` del código como propósito | La nota no añade nada a lo que ya se lee | Redactarlo en términos de negocio y de quién lo consume |
| Listar `System` y `Microsoft.Extensions` como dependencias | La tabla se llena de ruido | Solo dependencias con significado de diseño |
| Riesgos genéricos ("falta manejo de errores") | No accionable ni verificable | Archivo, línea aproximada y manifestación observable |
| Más de cinco riesgos o mejoras | Deja de ser una nota de una página | Lo que sobra pasa a la deuda técnica del README de módulo |
| Aceptar las líneas que indica Copilot sin abrir el archivo | La nota cita líneas inexistentes | Verificación uno a uno antes de entregar |
| Describir el retorno como precio total | Error de negocio propagado | `CalcularPrecioFinal` devuelve el precio **unitario** final |

---

# Plantilla 2 — Documentación XML de C#

**Propósito:** documentar los miembros públicos para que Swagger, IntelliSense y la revisión de código compartan la misma información y la compilación pueda exigirla.

| Campo | Valor |
|---|---|
| Cuándo se usa | Al declarar cualquier tipo o miembro público y al cerrar TODO-03 |
| Quién la escribe | Quien escribe el miembro; el revisor la comprueba en el Pull Request |
| Dónde vive | En el propio archivo `.cs`, con `///` |
| Prompt file asociado | `.github/prompts/documentar-xml.prompt.md` |

## Etiquetas

| Etiqueta | Obligatoriedad | Contenido |
|---|---|---|
| `<summary>` | Siempre en miembros públicos | Una frase que no repita el nombre del miembro |
| `<param name="...">` | Por cada parámetro | Significado, unidad y rango válido |
| `<returns>` | Si devuelve algo | Qué devuelve y, si es anulable, qué significa `null` |
| `<exception cref="...">` | Por cada excepción documentada | Condición exacta que la provoca |
| `<remarks>` | Cuando hay reglas de negocio | Topes, redondeos y orden de aplicación |
| `<inheritdoc />` | En la implementación de un miembro ya documentado | Evita duplicar y desincronizar el texto |
| `<see cref="..."/>` | Al citar otro tipo o miembro | Genera enlace y falla la compilación si el destino no existe |
| `<c>` | Al citar un literal en prosa | `<c>null</c>`, `<c>0.20m</c>` |
| `<example>` | En cálculos no evidentes | Un caso concreto con entradas y salida |

## Reglas de este repositorio

Provienen de `.github/instructions/csharp.instructions.md` y de `documentar-xml.prompt.md`.

- Texto en español; identificadores y textos del código **sin acentos**.
- `<summary>` en una sola frase que no reformule el nombre del miembro.
- `<param>` con significado y rango válido, no con la traducción del nombre.
- `<returns>` explica qué significa `null` cuando el tipo es anulable.
- Un `<exception cref="...">` por cada excepción que el miembro lanza de forma documentada.
- `<remarks>` para topes, redondeos y orden de aplicación de las reglas.
- Al documentar no se modifica ninguna firma ni ningún cuerpo: solo se añaden comentarios.

**Nota práctica:** al activar `<GenerateDocumentationFile>true</GenerateDocumentationFile>` en `src/Catalogo.Api/Catalogo.Api.csproj` junto con `-warnaserror` (`dotnet build CopilotLabCatalogo.sln -warnaserror`), cualquier miembro público sin documentar rompe la compilación con **CS1591**. En el estado inicial la propiedad está desactivada con un comentario `TODO-03`, precisamente porque las tres interfaces siguen sin documentar.

## Plantilla

```csharp
/// <summary><Que hace, en una frase, sin repetir el nombre del miembro.></summary>
/// <param name="<nombre>"><Significado, unidad y rango valido.></param>
/// <returns><Que devuelve; que significa <c>null</c> si puede devolverlo.></returns>
/// <exception cref="<TipoDeExcepcion>"><Condicion exacta que la provoca.></exception>
/// <remarks><Topes, redondeos y orden de aplicacion de las reglas.></remarks>
/// <example><Caso concreto: entradas y salida esperada.></example>
<firma del miembro>
```

## Ejemplo (a) — `IDescuentoCalculator.CalcularPrecioFinal` documentado íntegro

Es el TODO-03 de `src/Catalogo.Api/Services/IDescuentoCalculator.cs`, que hoy solo tiene el comentario del pendiente.

```csharp
namespace Catalogo.Api.Services;

/// <summary>Contrato de la politica de descuentos comerciales del catalogo.</summary>
public interface IDescuentoCalculator
{
    /// <summary>
    /// Aplica el descuento por volumen y el de cliente preferente sobre un precio de lista.
    /// </summary>
    /// <param name="precioBase">Precio unitario de lista, en la moneda del catalogo. Debe ser mayor que cero.</param>
    /// <param name="cantidad">Unidades de la operacion. Debe ser mayor o igual que 1.</param>
    /// <param name="esClientePreferente">Indica si la operacion corresponde a un cliente preferente.</param>
    /// <returns>
    /// El precio unitario final, redondeado a dos decimales. No es el importe total de la operacion.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Si <paramref name="precioBase"/> es menor o igual que cero, o si <paramref name="cantidad"/> es menor que 1.
    /// </exception>
    /// <remarks>
    /// Volumen: 5 % desde 10 unidades, 10 % desde 50 y 15 % desde 100. El cliente preferente suma 8
    /// puntos porcentuales. Los descuentos se suman, no se componen, y el total se topa en el 20 %.
    /// El redondeo usa <c>MidpointRounding.AwayFromZero</c>.
    /// </remarks>
    /// <example>
    /// Con <c>precioBase = 100</c>, <c>cantidad = 100</c> y cliente preferente el descuento nominal
    /// seria del 23 %, pero el tope lo deja en el 20 % y el resultado es <c>80.00</c>.
    /// </example>
    decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente);
}
```

## Ejemplo (b) — `IProductoService.ActualizarPrecioAsync` documentado íntegro

El retorno es `Task<ProductoResponse?>`: el `null` es parte del contrato y significa producto inexistente, según `csharp.instructions.md` ("recurso inexistente: se devuelve `null` desde el servicio y el controller responde 404").

```csharp
/// <summary>
/// Recalcula y persiste el precio de un producto aplicando la politica de descuentos vigente.
/// </summary>
/// <param name="id">Identificador del producto que se actualiza.</param>
/// <param name="solicitud">Precio de lista, cantidad de la operacion e indicador de cliente preferente.</param>
/// <param name="cancellationToken">Token para cancelar la operacion.</param>
/// <returns>
/// El producto actualizado con su precio final, o <c>null</c> si no existe ningun producto con ese
/// identificador. El <c>null</c> no indica error: el controller lo traduce a 404 con
/// <see cref="Microsoft.AspNetCore.Mvc.ProblemDetails"/>.
/// </returns>
/// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
/// Si <paramref name="solicitud"/> no supera la validacion de DataAnnotations.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// Si el precio base es menor o igual que cero o la cantidad es menor que 1; procede de
/// <see cref="IDescuentoCalculator.CalcularPrecioFinal"/>.
/// </exception>
/// <remarks>
/// El precio se calcula con <see cref="IDescuentoCalculator"/> y se persiste, de modo que una consulta
/// posterior con <see cref="ObtenerPorIdAsync"/> devuelve el valor nuevo.
/// </remarks>
Task<ProductoResponse?> ActualizarPrecioAsync(int id, ActualizarPrecioRequest solicitud, CancellationToken cancellationToken = default);
```

## Ejemplo (c) — `IProductoRepository` documentado y `<inheritdoc />` en la implementación

La interfaz concentra el texto y `InMemoryProductoRepository` lo hereda; es el patrón que ya sigue `ProductoService`.

```csharp
/// <summary>Acceso al almacen de productos del catalogo.</summary>
public interface IProductoRepository
{
    /// <summary>Recupera todos los productos, ordenados por identificador.</summary>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>La lista completa de productos; vacia si el almacen no tiene ninguno.</returns>
    Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>Busca un producto por su identificador.</summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>El producto, o <c>null</c> si no existe ninguno con ese identificador.</returns>
    Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Incorpora un producto nuevo y le asigna el siguiente identificador disponible.</summary>
    /// <param name="producto">Producto que se incorpora; su <c>Id</c> se sobrescribe.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>El producto ya persistido, con su identificador asignado.</returns>
    /// <exception cref="ArgumentNullException">Si <paramref name="producto"/> es <c>null</c>.</exception>
    Task<Producto> AgregarAsync(Producto producto, CancellationToken cancellationToken = default);
}

public sealed class InMemoryProductoRepository : IProductoRepository
{
    /// <inheritdoc />
    public Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _productos.TryGetValue(id, out var producto);
        return Task.FromResult(producto);
    }
}
```

## Reparto Copilot / persona y verificación

| Elemento | Copilot `[GA]` | Persona |
|---|---|---|
| Esqueleto de etiquetas, `<param>` por parámetro y `<summary>` inicial | Sí, con `/doc` o con `documentar-xml.prompt.md` | Comprueba que no falte ningún parámetro y reescribe el `<summary>` si repite el nombre |
| Rangos válidos en `<param>` | Propuesta a partir de las guardas del código | Confirma el rango contra la regla de negocio |
| Significado de `null` en `<returns>` | Suele omitirlo o generalizarlo | **Lo escribe la persona**: es contrato, no descripción |
| `<remarks>` con topes y redondeos | Habitualmente incompleto | **Lo escribe la persona**: los porcentajes y el tope del 20 % son datos de negocio |
| `<exception>` | Detecta las que ve en el cuerpo | Añade las heredadas de dependencias, como la de `IDescuentoCalculator` |

**Cómo verificar:** `dotnet build CopilotLabCatalogo.sln -warnaserror` con `GenerateDocumentationFile` activo debe dar cero CS1591; un `cref` roto produce CS1574 y delata texto inventado; `git diff` debe mostrar solo líneas añadidas que empiezan por `///`; Swagger debe publicar los textos en la operación correspondiente; si al quitar el nombre del método el `<summary>` deja de decir algo, se reescribe.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| `<summary>Calcula el precio final.</summary>` sobre `CalcularPrecioFinal` | Documentación que no informa | Describir la regla, no el nombre |
| `<param name="cantidad">La cantidad.</param>` | No aporta el rango válido | "Unidades de la operacion. Debe ser mayor o igual que 1." |
| Omitir el significado de `null` en un retorno anulable | Quien consume confunde `null` con error | Explicitar "producto inexistente" y su traducción a 404 |
| Duplicar el texto en interfaz e implementación | Los dos textos divergen al primer cambio | `<inheritdoc />` en la implementación |
| Escribir acentos dentro del código | Rompe la convención del repositorio | Prosa acentuada solo fuera del código |
| Documentar sin activar `GenerateDocumentationFile` | Nada obliga a mantener la documentación | Activar la propiedad y compilar con `-warnaserror` |
| Cambiar el cuerpo del método "de paso" | El PR de documentación mezcla comportamiento | Revertir y separar en dos commits |

---

# Plantilla 3 — ADR breve (una página)

**Propósito:** registrar una decisión de arquitectura con su contexto, las alternativas descartadas y la forma de comprobar que se sostiene, para que dentro de un año se entienda por qué el código es como es.

| Campo | Valor |
|---|---|
| Cuándo se usa | Ante decisiones con coste de reversión: fronteras entre capas, contratos públicos, dependencias nuevas |
| Quién la escribe | Quien propone la decisión; se acepta en revisión con el equipo |
| Dónde vive | `docs/adr/ADR-NNN-<titulo-en-kebab-case>.md` |

## Plantilla

```markdown
# ADR-<NNN> — <Título en una frase afirmativa>
- **Fecha:** <AAAA-MM-DD>
- **Estado:** <Propuesta | Aceptada | Sustituida por ADR-NNN | Rechazada>
- **Responsables:** <quién propone> / <quién aprueba>

## Contexto
<Situación, restricciones y fuerzas en juego. Hechos, no opiniones.>

## Decisión
<Una frase en presente afirmativo: "Se hace X".>

## Alternativas consideradas
| Alternativa | Ventajas | Inconvenientes | Por qué no |
|---|---|---|---|
| <opción> | <ventajas> | <inconvenientes> | <motivo real del descarte> |

## Consecuencias
**Positivas**
- <consecuencia observable>

**Negativas**
- <coste asumido conscientemente>

## Cómo se verificará
- <Prueba, comando o métrica que demuestra que la decisión se sostiene.>
```

## Ejemplo rellenado: ADR-001

```markdown
# ADR-001 — El cálculo de descuentos vive en `IDescuentoCalculator` y no en `ProductoService`
- **Fecha:** 2026-08-14
- **Estado:** Aceptada
- **Responsables:** equipo del catálogo / responsable técnico

## Contexto
La política comercial define descuentos por volumen (5 % desde 10 unidades, 10 % desde 50, 15 % desde
100), un 8 % adicional para cliente preferente, suma de descuentos y un tope global del 20 %. Estas
reglas cambian por decisión comercial con más frecuencia que el resto del catálogo. `ProductoService`
ya concentra la orquestación del repositorio, el mapeo a `ProductoResponse` y el registro de eventos.
El estado inicial contiene además dos defectos en estas reglas (BUG-01), lo que hace crítico poder
probarlas de forma aislada.

## Decisión
El cálculo del precio final se expone en `IDescuentoCalculator`, con la única operación
`decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente)`, y se
implementa en `DescuentoCalculator`. `ProductoService` la recibe por constructor y no reproduce
ninguna regla de descuento.

## Alternativas consideradas
| Alternativa | Ventajas | Inconvenientes | Por qué no |
|---|---|---|---|
| Lógica embebida en `ProductoService` | Un archivo menos, sin indirección | Para probar un umbral hay que construir el servicio con `IProductoRepository` e `ILogger` simulados; las reglas se mezclan con la orquestación | Encarece cada prueba de umbral y convierte un defecto de cálculo en un defecto del servicio |
| Extension method estático sobre `decimal` | Sintaxis cómoda en el punto de uso | No se puede sustituir por un doble de prueba ni registrar en el contenedor; imposible variar la política por entorno | Impide aislar la regla y bloquea la evolución hacia política configurable |
| Motor de reglas configurable | Cambiar porcentajes sin recompilar | Exige un formato de reglas, su validación y su documentación; el equipo no tiene hoy esa necesidad | Coste desproporcionado para cuatro porcentajes y un tope |

## Consecuencias
**Positivas**
- Las reglas se prueban sin levantar el servicio: la prueba
  `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` se escribe con
  `new DescuentoCalculator()` y una sola llamada, sin repositorio ni `ILogger`.
- `DescuentoCalculator` no tiene estado mutable, por lo que el registro como singleton de `Program.cs`
  línea 16 es seguro.
- `ProductoService` puede probarse con un doble de `IDescuentoCalculator`, sin depender de los
  porcentajes vigentes.
- La corrección de BUG-01 queda contenida en un solo archivo.

**Negativas**
- Un archivo y una interfaz adicionales para una operación de una sola firma.
- La regla queda a un salto de indirección del punto donde se lee el precio.
- El tope del 20 % vive dentro de la implementación: cambiarlo exige recompilar y desplegar.

## Cómo se verificará
- `dotnet test --filter DescuentoCalculatorTests` cubre umbrales, tope, redondeo y entradas inválidas
  sin instanciar `ProductoService`.
- Ninguna clase fuera de `Services/` contiene los literales `0.05m`, `0.10m`, `0.15m`, `0.08m` ni
  `0.20m`: se comprueba con `git grep`.
- `ProductosController` no realiza ningún cálculo, según `.github/instructions/api.instructions.md`.
```

## Reparto Copilot / persona y verificación

| Apartado | Copilot `[GA]` | Persona |
|---|---|---|
| Contexto | Resumen del código y de las restricciones visibles | Añade las fuerzas invisibles: frecuencia de cambio comercial, plazos |
| Decisión | Reformula lo que el código ya hace | **La decisión y su alcance los fija la persona** |
| Alternativas y "Por qué no" | Genera opciones plausibles, con motivos genéricos | Elimina las que nadie evaluó y escribe el motivo real del descarte |
| Consecuencias negativas | Tiende a omitirlas o suavizarlas | **Obligatorio que las escriba la persona**: un ADR sin coste asumido no es un ADR |
| Verificación | Propone comandos | Confirma que el comando existe y pasa |

**Cómo verificar:** comprobar que cada alternativa listada se consideró de verdad; ejecutar el comando de "Cómo se verificará" y pegar el resultado en el Pull Request; contrastar el ADR con el código (si dice singleton, `Program.cs` debe registrar singleton); confirmar que la sección de consecuencias negativas no está vacía.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| Escribir el ADR después de implementar y describir solo el resultado | Se pierde la razón de la decisión | Registrar contexto y fuerzas, no el diff |
| Alternativas de relleno que nadie evaluó | El ADR pierde credibilidad | Solo alternativas realmente consideradas |
| Consecuencias negativas ausentes | Parece un documento de venta | Enumerar el coste asumido |
| Estado permanentemente en "Propuesta" | Nadie sabe si rige | Actualizar a Aceptada, Rechazada o Sustituida por ADR-NNN |
| Editar un ADR aceptado para cambiar la decisión | Se borra la historia | Crear un ADR nuevo y marcar el anterior como sustituido |
| Verificación redactada como intención ("se probará bien") | No es comprobable | Un comando concreto y su resultado esperado |

---

# Plantilla 4 — Nota de endpoint de API

**Propósito:** describir un endpoint con detalle suficiente para implementarlo, probarlo y consumirlo sin abrir el código: contrato, códigos de estado, reglas de negocio y casos borde.

| Campo | Valor |
|---|---|
| Cuándo se usa | Antes de implementar un endpoint nuevo y al modificar un contrato existente |
| Quién la escribe | Quien implementa; se revisa con quien consume la API |
| Dónde vive | `docs/api/<verbo>-<ruta>.md`, con su resumen en la XML doc de la acción |
| Convenciones | `.github/instructions/api.instructions.md` |

## Plantilla

```markdown
# <VERBO> <ruta>

## Propósito de negocio
<Una frase.>

## Parámetros
| Ubicación | Nombre | Tipo | Obligatorio | Restricciones | Valor por defecto |
|---|---|---|---|---|---|
| <ruta/query/cuerpo> | <nombre> | <tipo> | <Sí/No> | <restricción> | <valor o guion> |

## Cuerpo de respuesta
<ejemplo JSON del caso correcto>

## Códigos de estado
| Código | Cuándo se produce | Cuerpo |
|---|---|---|
| <nnn> | <condición> | <tipo, o ProblemDetails con su Title> |

## Reglas de negocio aplicadas
1. <regla>

## Idempotencia y efectos secundarios
- **Idempotente:** <Sí/No> — <justificación>
- **Efectos:** <qué se persiste, qué se registra en el log>

## Ejemplos
<curl de la petición y respuesta literal con su línea de estado>

## Casos borde
| Caso | Resultado esperado |
|---|---|

## Atributos esperados en la acción
<lista de [ProducesResponseType] y atributo de ruta>
```

## Ejemplo rellenado: `PATCH /api/productos/{id:int}/precio`

Corresponde a TODO-01 y a la historia HU-01. En el estado inicial la ruta no existe y `ProductoService.ActualizarPrecioAsync` lanza `NotImplementedException` (línea 71).

```markdown
# PATCH /api/productos/{id:int}/precio

## Propósito de negocio
Permite al administrador del catálogo recalcular y persistir el precio de un producto aplicando la
política de descuentos vigente.

## Parámetros
| Ubicación | Nombre | Tipo | Obligatorio | Restricciones | Valor por defecto |
|---|---|---|---|---|---|
| Ruta | `id` | `int` | Sí | Restricción `{id:int}`; debe existir en el catálogo | — |
| Cuerpo | `precioBase` | `decimal` | Sí | Mayor que cero | — |
| Cuerpo | `cantidad` | `int` | Sí | Mayor o igual que 1 | — |
| Cuerpo | `esClientePreferente` | `bool` | No | — | `false` |

El cuerpo se enlaza a `ActualizarPrecioRequest`, un `record` inmutable con `PrecioBase`, `Cantidad` y
`EsClientePreferente`.

## Cuerpo de respuesta
{"id":1,"nombre":"Monitor curvo 27 pulgadas","categoria":0,"precioBase":95.00,"existencias":14,"activo":true}

## Códigos de estado
| Código | Cuándo se produce | Cuerpo |
|---|---|---|
| 200 | El producto existe y los datos son válidos | `ProductoResponse` con el precio recalculado |
| 400 | `precioBase <= 0` o `cantidad < 1` | `ProblemDetails` con `Title: "Datos invalidos"` y el campo infractor |
| 404 | No existe producto con ese `id` | `ProblemDetails` con `Title: "Producto no encontrado"` y `Detail: "No existe un producto con el identificador {id}."` |

## Reglas de negocio aplicadas
1. Volumen: 5 % desde 10 unidades, 10 % desde 50 y 15 % desde 100, con comparación mayor o igual.
2. El cliente preferente suma 8 puntos porcentuales.
3. Los descuentos se suman y el total se topa en el 20 %.
4. Se redondea a dos decimales con `MidpointRounding.AwayFromZero`.
5. El valor persistido en `PrecioBase` es el precio unitario final, no el importe total.

## Idempotencia y efectos secundarios
- **Idempotente:** No. Repetir la misma petición con el mismo cuerpo deja el mismo estado, pero
  encadenar llamadas que usen el precio ya rebajado como nuevo `precioBase` acumula descuentos. La
  regla es enviar siempre el precio de lista.
- **Efectos:** se modifica `Producto.PrecioBase` en el repositorio; el cambio es visible de inmediato
  en `GET /api/productos/{id}` porque `InMemoryProductoRepository` es singleton.

## Ejemplos
curl -i -X PATCH "http://localhost:5080/api/productos/1/precio" \
  -H "Content-Type: application/json" \
  -d '{"precioBase":100.00,"cantidad":11,"esClientePreferente":false}'

HTTP/1.1 200 OK
{"id":1,"nombre":"Monitor curvo 27 pulgadas","categoria":0,"precioBase":95.00,"existencias":14,"activo":true}

curl -i -X PATCH "http://localhost:5080/api/productos/9999/precio" \
  -H "Content-Type: application/json" \
  -d '{"precioBase":100.00,"cantidad":11,"esClientePreferente":false}'

HTTP/1.1 404 Not Found
{"title":"Producto no encontrado","detail":"No existe un producto con el identificador 9999.","status":404}

## Casos borde
| Caso | Resultado esperado |
|---|---|
| `cantidad = 9` | 200 con el precio enviado: no hay descuento |
| `cantidad = 10` | 200 con 5 %: 100,00 devuelve 95,00 |
| `cantidad = 100` y cliente preferente | 200 con 80,00: 15 % + 8 % topado al 20 % |
| `precioBase = 0` o `cantidad = 0` | 400 |
| `/api/productos/abc/precio` | 404 por la restricción de ruta `{id:int}` |
| Cuerpo ausente | 400 |

## Atributos esperados en la acción
[HttpPatch("{id:int}/precio")]
[ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
public async Task<ActionResult<ProductoResponse>> ActualizarPrecioAsync(
    int id, [FromBody] ActualizarPrecioRequest solicitud, CancellationToken cancellationToken)
```

## Segundo ejemplo, en forma breve: `GET /api/productos`

En el estado inicial devuelve **500** porque `ProductoService.BuscarAsync` lanza `NotImplementedException` (TODO-02, línea 37). La nota describe el contrato objetivo.

| Ubicación | Nombre | Tipo | Obligatorio | Restricciones | Valor por defecto |
|---|---|---|---|---|---|
| Query | `categoria` | `Categoria?` | No | `0` Electronica, `1` Hogar, `2` Oficina, `3` Consumible | Sin filtro |
| Query | `precioMinimo` | `decimal?` | No | Cota inferior inclusiva | Sin filtro |
| Query | `precioMaximo` | `decimal?` | No | Cota superior inclusiva | Sin filtro |
| Query | `texto` | `string?` | No | Coincidencia parcial en el nombre, sin distinguir mayúsculas | Sin filtro |
| Query | `pagina` | `int` | No | Mínimo 1 | `1` |
| Query | `tamanoPagina` | `int` | No | Máximo 100 (`TamanoPaginaMaximo`) | `20` (`TamanoPaginaPredeterminado`) |

| Código | Cuándo se produce | Cuerpo |
|---|---|---|
| 200 | Consulta válida, aunque no haya coincidencias | `IReadOnlyList<ProductoResponse>`, posiblemente vacío |
| 400 | Parámetros de paginación fuera de rango | `ProblemDetails` |

```bash
curl -s "http://localhost:5080/api/productos?categoria=2&precioMinimo=400&texto=escritorio&pagina=1&tamanoPagina=20"
```

Casos borde: filtros que no devuelven nada producen 200 con lista vacía, no 404; `tamanoPagina=500` se rechaza o se topa en 100 según la decisión que se registre; una página posterior a la última devuelve 200 con lista vacía.

## Reparto Copilot / persona y verificación

| Apartado | Copilot `[GA]` | Persona |
|---|---|---|
| Tabla de parámetros | La deriva del DTO con fiabilidad alta | Comprueba obligatoriedad y valores por defecto contra el `record` |
| Ejemplos `curl` | Los genera completos | Verifica el puerto `http://localhost:5080` (definido en `appsettings.json`) y ejecuta al menos uno |
| Códigos de estado | Propone el conjunto habitual | **Decide cuál corresponde a cada regla**: 400 frente a 422, o 404 frente a 200 vacío, es diseño |
| Reglas de negocio | Las copia si tiene el archivo en contexto | Confirma porcentajes, tope y redondeo con la fuente de negocio |
| Idempotencia | Suele afirmarla sin analizarla | La analiza la persona: aquí el efecto acumulativo no es evidente |
| Casos borde | Genera los obvios | Añade los de umbral 9/10, 49/50, 99/100, que son los que rompen |

**Cómo verificar:** ejecutar cada `curl` contra la API en marcha y comparar la respuesta literal con su línea de estado; contrastar la tabla de códigos con los `[ProducesResponseType]` de la acción, uno a uno; comprobar que Swagger publica los mismos códigos y esquema; verificar que ningún ejemplo usa un campo ajeno a `ProductoResponse` (`Id`, `Nombre`, `Categoria`, `PrecioBase`, `Existencias`, `Activo`); confirmar que el controller sigue sin lógica de negocio.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| Documentar 200 y olvidar 400 y 404 | El consumidor no maneja los fallos | Una fila por cada código que la acción declara |
| Usar `PUT` donde el diseño pide `PATCH` | Se sobrescribe todo el recurso | La actualización parcial de precio es `PATCH` |
| Omitir la restricción `{id:int}` | Cambia el comportamiento ante `/api/productos/abc/precio` | Declarar la restricción tipada en la ruta |
| Ejemplos `curl` con un puerto inventado | El participante no puede reproducirlos | `http://localhost:5080`, según `appsettings.json` |
| Declararlo idempotente sin analizarlo | Reintentos automáticos que acumulan descuentos | Analizar y documentar el efecto de la llamada repetida |
| Documentar el retorno como importe total | Error de negocio en el contrato público | `PrecioBase` recibe el precio **unitario** final |
| Responder 404 cuando una búsqueda no encuentra nada | Confunde recurso inexistente con resultado vacío | 200 con lista vacía |

---

# Plantilla 5 — README de módulo

**Propósito:** dar entrada al módulo a quien llega de nuevo: qué responsabilidad tiene, qué no le corresponde, qué expone, cómo se registra, cómo se prueba y qué deuda arrastra.

| Campo | Valor |
|---|---|
| Cuándo se usa | Al crear una carpeta con responsabilidad propia; se actualiza en cada cambio de contrato |
| Quién la escribe | La persona propietaria del módulo |
| Dónde vive | `README.md` dentro de la propia carpeta |

## Plantilla

```markdown
# Módulo <nombre>
**Responsabilidad:** <una frase.>

## Qué NO es responsabilidad de este módulo
- <responsabilidad que pertenece a otra capa, nombrando esa capa>

## Tipos públicos
| Tipo | Clase de tipo | Responsabilidad |
|---|---|---|

## Dependencias
| Depende de | Para qué |
|---|---|

## Registro en la inyección de dependencias
<líneas exactas de Program.cs, con su número de línea>

## Cómo se prueba
<comando exacto y recuento esperado>

## Invariantes
1. <afirmación que debe cumplirse siempre y que puede comprobarse con una prueba>

## Deuda técnica conocida
| Id | Descripción | Impacto | Dónde |
|---|---|---|---|
```

## Ejemplo rellenado: `src/Catalogo.Api/Services/`

```markdown
# Módulo Services
**Responsabilidad:** concentrar las reglas de negocio del catálogo (orquestación de productos y
política de descuentos) entre el transporte HTTP y el almacenamiento.

## Qué NO es responsabilidad de este módulo
- Enlazar peticiones HTTP ni producir códigos de estado: pertenece a `Controllers/`.
- Persistir o consultar el almacén: pertenece a `Repositories/`.
- Definir los contratos de entrada y salida: pertenece a `Dtos/`.
- Llamar al servicio legado de precios: pertenece a `Infrastructure/LegacyPricingClient.cs`.
- Devolver entidades `Producto` hacia fuera: al exterior siempre sale `ProductoResponse`.

## Tipos públicos
| Tipo | Clase de tipo | Responsabilidad |
|---|---|---|
| `IProductoService` | Interfaz | Contrato de negocio: buscar, obtener, crear, actualizar precio y eliminar |
| `ProductoService` | Clase `sealed` | Orquesta repositorio y calculadora, y mapea a `ProductoResponse` |
| `IDescuentoCalculator` | Interfaz | Contrato de la política de descuentos |
| `DescuentoCalculator` | Clase `sealed` | Descuentos por volumen y por cliente preferente |

## Dependencias
| Depende de | Para qué |
|---|---|
| `Catalogo.Api.Repositories.IProductoRepository` | Leer y persistir productos |
| `Catalogo.Api.Dtos` | `CrearProductoRequest`, `ActualizarPrecioRequest`, `BusquedaProductosQuery`, `ProductoResponse` |
| `Catalogo.Api.Models` | Entidad `Producto` y enum `Categoria` |
| `ILogger<ProductoService>` | Registrar creación y eliminación de productos |

`DescuentoCalculator` no depende de nada: sus cuatro porcentajes son constantes del propio archivo.

## Registro en la inyección de dependencias
En `src/Catalogo.Api/Program.cs`, líneas 15 a 18:

    builder.Services.AddSingleton<IProductoRepository, InMemoryProductoRepository>();
    builder.Services.AddSingleton<IDescuentoCalculator, DescuentoCalculator>();
    builder.Services.AddSingleton<LegacyPricingClient>();
    builder.Services.AddScoped<IProductoService, ProductoService>();

`DescuentoCalculator` es singleton porque no tiene estado mutable. `ProductoService` es scoped: una
instancia por petición.

## Cómo se prueba
    dotnet test CopilotLabCatalogo.sln
    dotnet test --filter DescuentoCalculatorTests
    dotnet test --filter ProductoServiceTests

Estado esperado en el repositorio inicial: **15 pruebas superadas y 2 omitidas**. Las omitidas llevan
`[Fact(Skip = ...)]`: una expone BUG-01 y la otra cubre TODO-01.

## Invariantes
1. `CalcularPrecioFinal` devuelve siempre el precio unitario, redondeado a dos decimales.
2. El descuento acumulado nunca supera el 20 % una vez corregido BUG-01.
3. `ProductoService` nunca devuelve la entidad `Producto`: siempre `ProductoResponse`.
4. Un recurso inexistente se comunica devolviendo `null`, nunca lanzando una excepción.
5. Todo método asíncrono público termina en `Async` y recibe `CancellationToken` como último parámetro
   con valor por defecto.
6. `DescuentoCalculator` no tiene estado mutable, condición necesaria para su registro como singleton.

## Deuda técnica conocida
| Id | Descripción | Impacto | Dónde |
|---|---|---|---|
| BUG-01 | Umbrales con `>` en vez de `>=` y tope del 20 % no aplicado | Precios incorrectos en 10, 50 y 100 unidades y descuentos por encima del 20 % | `Services/DescuentoCalculator.cs` 32-41 y 44-62 |
| TODO-01 | `ActualizarPrecioAsync` lanza `NotImplementedException` y falta el endpoint `PATCH` | HU-01 sin cubrir | `Services/ProductoService.cs` 71-75 y `Controllers/ProductosController.cs` 90-92 |
| TODO-02 | `BuscarAsync` lanza `NotImplementedException` | `GET /api/productos` responde 500 | `Services/ProductoService.cs` 37-41 |
| TODO-03 | `IProductoService` e `IDescuentoCalculator` sin documentación XML | `GenerateDocumentationFile` no puede activarse | `Services/IProductoService.cs` 5 y `Services/IDescuentoCalculator.cs` 3 |
| BUG-02 | `CrearAsync` no valida la solicitud | `POST` con nombre vacío y precio negativo devuelve 201 | `Services/ProductoService.cs` 52-68 |
```

## Reparto Copilot / persona y verificación

| Apartado | Copilot `[GA]` | Persona |
|---|---|---|
| Tipos públicos y dependencias | Inventario fiable si la carpeta está en contexto | Verifica que no falte ninguno y descarta las dependencias sin valor informativo |
| Registro en DI | Copia las líneas de `Program.cs` | **Explica por qué singleton y por qué scoped**: eso no está en el código |
| Comando de prueba | Propone `dotnet test` | Lo ejecuta y anota el recuento real: 15 superadas y 2 omitidas |
| "Qué NO es responsabilidad" | Rara vez lo produce bien | **Lo escribe la persona**: define la frontera del módulo |
| Invariantes y deuda técnica | Puede inferir alguna y detecta `TODO` y `NotImplementedException` | Confirma o refuta cada invariante con una prueba y valora el impacto de la deuda |

**Cómo verificar:** comparar la tabla de tipos con el listado real de la carpeta; ejecutar los tres comandos y comprobar el recuento; abrir `Program.cs` y verificar línea por línea el bloque citado; comprobar cada invariante con una prueba existente o marcarla como no verificada; confirmar que cada elemento de deuda apunta a un `TODO` o defecto real.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| Omitir "qué NO es responsabilidad" | El módulo acumula lógica ajena con el tiempo | Es el apartado que más protege la arquitectura; nunca se omite |
| Copiar el registro de DI sin explicar los ciclos de vida | Nadie sabe por qué singleton y no scoped | Justificar cada elección en una frase |
| `dotnet test` sin filtro ni recuento esperado | No se detecta una regresión | Comando exacto más resultado esperado |
| Confundir invariante con deseo ("debería ser rápido") | Sección inútil | Una invariante se comprueba con una prueba |
| Dejar la deuda técnica vacía porque "queda mal" | El módulo parece sano y no lo está | Enumerar BUG y TODO con su ubicación |
| No actualizar el README al cambiar un contrato | Documentación que engaña | Incluir el README en la lista de verificación del Pull Request |

---

# Plantilla 6 — Nota de causa raíz de un defecto

**Propósito:** separar el síntoma de la causa, dejar constancia de la investigación, incluidas las hipótesis descartadas, y fijar la prueba que impide que el defecto vuelva.

| Campo | Valor |
|---|---|
| Cuándo se usa | Tras corregir cualquier defecto con impacto funcional |
| Quién la escribe | Quien corrige; se adjunta al Pull Request de la corrección |
| Dónde vive | `docs/defectos/<ID>.md` o el cuerpo del Pull Request |

## Plantilla

```markdown
# <ID> — <Título del defecto>
- **Fecha:** <AAAA-MM-DD> · **Autor:** <nombre> · **Estado:** <Abierto | Corregido | Cerrado con regresión cubierta>

## Síntoma observable
<Qué se ve, con el comando o la prueba que lo reproduce y su salida literal.>

## Impacto y alcance
| Dimensión | Valor |
|---|---|
| Quién se ve afectado | <usuarios, entornos> |
| Desde cuándo | <versión o commit> |
| Severidad | <Alta/Media/Baja> |

## Línea de tiempo
| Momento | Hecho |
|---|---|

## Investigación
| # | Hipótesis | Cómo se probó | Resultado |
|---|---|---|---|

## Causa raíz
<Una sola frase. No es el síntoma: es la razón por la que el síntoma existe.>

## Corrección aplicada
<diff conceptual: lo mínimo que cambia el comportamiento>

## Prueba que impide la regresión
<prueba con nombre explícito>

## Por qué no se detectó antes
<Qué hueco de las pruebas o del proceso lo permitió.>

## Acciones preventivas
| # | Acción | Responsable |
|---|---|---|
```

## Ejemplo rellenado: BUG-01

```markdown
# BUG-01 — El descuento no se aplica en los umbrales exactos y no respeta el tope del 20 %
- **Fecha:** 2026-08-14 · **Autor:** <participante> · **Estado:** Corregido con regresión cubierta

## Síntoma observable
La prueba `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` está
marcada con `[Fact(Skip = "Habilitar en el Laboratorio 6")]`. Al quitar el argumento `Skip` y ejecutar
`dotnet test --filter CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento`,
la prueba falla porque **se esperaba 80,00 pero se obtuvo 82,00**. Segundo síntoma, independiente: con
`precioBase = 100` y `cantidad = 10` el método devuelve 100,00 en lugar de 95,00, es decir, no aplica
ningún descuento en el umbral exacto.

## Impacto y alcance
| Dimensión | Valor |
|---|---|
| Quién se ve afectado | Toda operación con cantidad 10, 50 o 100, y todo cliente preferente con más de 50 unidades |
| Desde cuándo | Desde la implementación inicial de `DescuentoCalculator` |
| Severidad | Alta: el precio calculado difiere de la política comercial en ambos sentidos |

## Línea de tiempo
| Momento | Hecho |
|---|---|
| T0 | Se implementa `DescuentoCalculator` con la cadena de comparaciones y sin constante de tope |
| T1 | Las pruebas iniciales usan 11, 51 y 101 unidades: no tocan ningún umbral exacto y pasan |
| T2 | Se añade la prueba del tope y se deja con `Skip` para no bloquear la rama |
| T3 | Se quita el `Skip`: la prueba falla con 82,00 frente a 80,00 |
| T4 | Se corrigen los umbrales y se añade el tope; toda la suite queda en verde |

## Investigación
| # | Hipótesis | Cómo se probó | Resultado |
|---|---|---|---|
| 1 | El redondeo pierde precisión y produce 82,00 | Cálculo a mano: 100 × (1 − 0,18) = 82,00 exacto; `Math.Round` con `AwayFromZero` no interviene | **Descartada**: el desvío es de 2,00, no de céntimos |
| 2 | El descuento de cliente preferente se compone en vez de sumarse | Lectura de la línea 36: `descuento += DescuentoClientePreferente`, que es suma | **Descartada**: la suma es correcta |
| 3 | `ObtenerDescuentoPorVolumen(100)` no devuelve 0,15 | Lectura de la línea 46: `if (cantidad > 100)` es falso con 100, así que cae en el tramo de 0,10 | **Confirmada**: 0,10 + 0,08 = 0,18, de donde sale 82,00 |
| 4 | Existe un tope que no se aplica | Búsqueda de `Tope` en todo el proyecto: la constante no existe | **Confirmada**: no hay tope de ningún tipo |

## Causa raíz
Hay **dos causas raíz independientes** en el mismo archivo:
1. Los tres umbrales de `ObtenerDescuentoPorVolumen` usan el comparador estricto `>` cuando la regla es
   "mayor o igual", de modo que 10, 50 y 100 unidades caen en el tramo inferior.
2. `CalcularPrecioFinal` suma el 8 % del cliente preferente al descuento por volumen y usa el resultado
   sin limitarlo: la constante `TopeDescuento = 0.20m` nunca se implementó.

El síntoma es "devuelve 82,00 en lugar de 80,00"; las causas son el comparador y la ausencia del tope.
Corregir solo una deja la otra viva: con `cantidad = 120` y cliente preferente el descuento llegaría al
23 % aunque los umbrales fueran correctos.

## Corrección aplicada
      private const decimal DescuentoClientePreferente = 0.08m;
    + private const decimal TopeDescuento = 0.20m;

      if (esClientePreferente) { descuento += DescuentoClientePreferente; }
    + if (descuento > TopeDescuento) { descuento = TopeDescuento; }
      var precioFinal = precioBase * (1m - descuento);

    - if (cantidad > 100) { return DescuentoVolumenAlto; }
    + if (cantidad >= 100) { return DescuentoVolumenAlto; }
    - if (cantidad > 50)  { return DescuentoVolumenMedio; }
    + if (cantidad >= 50)  { return DescuentoVolumenMedio; }
    - if (cantidad > 10)  { return DescuentoVolumenBajo; }
    + if (cantidad >= 10)  { return DescuentoVolumenBajo; }

## Prueba que impide la regresión
La barrera son las pruebas de umbral a los dos lados de cada frontera, más la prueba del tope.

    [Theory]
    [InlineData(100.00, 9, 100.00)]
    [InlineData(100.00, 10, 95.00)]
    [InlineData(100.00, 49, 95.00)]
    [InlineData(100.00, 50, 90.00)]
    [InlineData(100.00, 99, 90.00)]
    [InlineData(100.00, 100, 85.00)]
    public void CalcularPrecioFinal_EnLosUmbralesExactos_AplicaElTramoCorrecto(
        double precioBase, int cantidad, double esperado)
    {
        var calculadora = new DescuentoCalculator();
        var resultado = calculadora.CalcularPrecioFinal((decimal)precioBase, cantidad, false);
        resultado.Should().Be((decimal)esperado);
    }

    [Fact]
    public void CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento()
    {
        var calculadora = new DescuentoCalculator();
        var resultado = calculadora.CalcularPrecioFinal(100m, 100, true);
        resultado.Should().Be(80.00m);
    }

La segunda prueba ya existía en `tests/Catalogo.Api.Tests/DescuentoCalculatorTests.cs`: la corrección
consiste en quitarle el `Skip`.

## Por qué no se detectó antes
Las pruebas existentes usaban 1, 9, 11, 51 y 101 unidades. Con esos valores `>` y `>=` producen el mismo
resultado, así que el defecto era invisible. La única prueba que tocaba el tope estaba desactivada con
`Skip`, y una prueba omitida no falla: el informe la cuenta como omitida, no como fallida.

## Acciones preventivas
| # | Acción | Responsable |
|---|---|---|
| 1 | Toda regla con umbral se prueba a los dos lados de la frontera: n−1 y n | Quien implementa la regla |
| 2 | Ninguna prueba entra en la rama principal con `Skip` sin fecha de reactivación | Revisor del Pull Request |
| 3 | Los topes se expresan como constante con nombre, nunca como número suelto | Quien implementa |
| 4 | En cada Pull Request se revisa el recuento de omitidas, no solo el de fallidas | Revisor |
```

## Reparto Copilot / persona y verificación

| Apartado | Copilot `[GA]` | Persona |
|---|---|---|
| Síntoma | Reformula el mensaje de fallo | Pega la salida literal del comando, sin editarla |
| Hipótesis y columna "Cómo se probó" | Genera candidatas plausibles; no puede decir cómo se probaron porque no ejecutó nada | **Decide cuáles probar y en qué orden** y escribe lo que realmente ejecutó |
| Hipótesis descartadas | Tiende a omitirlas y saltar a la conclusión | **Documentarlas es obligación de la persona**: es lo que da valor a la nota |
| Causa raíz | Suele confundirla con el síntoma | Una frase que explique el porqué, no el qué |
| Prueba de regresión | Genera el esqueleto y los `InlineData` | **Verifica cada valor esperado a mano**: una prueba generada puede consagrar el comportamiento defectuoso |
| Acciones preventivas | Propone genéricas | Las adapta al proceso real del equipo |

**Cómo verificar:** ejecutar la prueba **antes** de corregir y comprobar que falla con el mensaje citado (una prueba que ya pasaba no protege de nada); aplicar la corrección y verla en verde; ejecutar la suite completa sin regresiones; recalcular a mano cada `InlineData` a partir de la regla escrita; releer la causa raíz aislada y reescribirla si describe lo que se ve en pantalla; confirmar que el diff no incluye cambios ajenos.

## Errores frecuentes al rellenarla

| Error | Consecuencia | Corrección |
|---|---|---|
| Escribir como causa raíz "devolvía 82,00 en lugar de 80,00" | Eso es el síntoma; la nota no explica nada | La causa es el comparador `>` y la ausencia de la constante de tope |
| Detenerse en la primera causa encontrada | Se corrige el umbral y el tope sigue ausente | BUG-01 tiene dos causas independientes; buscar siempre si hay más |
| Omitir las hipótesis descartadas | Quien investigue después repetirá el trabajo | Documentar hipótesis, prueba y motivo del descarte |
| Aceptar los `InlineData` generados | La prueba puede fijar el comportamiento defectuoso como correcto | Calcular cada valor esperado desde la regla escrita |
| Corregir sin ver antes la prueba en rojo | No hay evidencia de que la prueba cubra el defecto | Rojo, corrección, verde, en ese orden |
| Acciones preventivas de relleno ("tener más cuidado") | No cambian nada | Acciones verificables, con responsable |
| No explicar por qué no se detectó antes | El hueco de las pruebas sigue abierto | Aquí: solo se probaban valores lejos de los umbrales y la prueba del tope estaba con `Skip` |

---

## Qué plantilla usar en cada laboratorio

| Laboratorio | Jornada | Plantillas | Artefacto sobre el repositorio | Evidencia que se entrega |
|---|---|---|---|---|
| **LAB-04** — Análisis y documentación de `DescuentoCalculator` | 2 | 1 y 2 | Nota de `Services/DescuentoCalculator.cs`; XML docs de `IDescuentoCalculator` e `IProductoService` (TODO-03) | Nota con los cinco riesgos, BUG-01 entre ellos con su línea; diff con las etiquetas `///`; salida de `dotnet build` sin CS1591 |
| **LAB-05** — Implementar `PATCH /api/productos/{id}/precio` | 3 | 4 y 2 | Nota del endpoint de HU-01 escrita antes del código; XML docs y `[ProducesResponseType]` de la acción | Nota con la tabla de códigos 200/400/404; salida de los `curl` de 200 y de 404; `dotnet test` con la prueba de TODO-01 reactivada |
| **LAB-06** — Refactorizar y corregir BUG-01 | 3 | 6 y 3 | Nota de causa raíz de BUG-01; ADR-001 sobre la separación de `IDescuentoCalculator` | Salida en rojo con "se esperaba 80,00 pero se obtuvo 82,00"; diff de la corrección; suite completa en verde; ADR-001 con sus tres alternativas |
| **LAB-08** — Seguridad, Pull Request y revisión asistida | 4 | 1 y 6 | Nota de `Infrastructure/LegacyPricingClient.cs` (secreto simulado de la línea 9 y su registro en el log); nota de causa raíz de BUG-02 | Nota con el riesgo de secreto y su línea; nota de BUG-02 con el `curl` que devuelve 201; enlace al Pull Request con la revisión de Copilot code review `[GA]`, que siempre es de tipo comentario |
| **LAB-09** — Instrucciones del repositorio y guía de equipo | 5 | 5 y 3 | README de `src/Catalogo.Api/Services/`; ADR de la política de personalización del equipo | README con tipos públicos, registro en DI, comando de pruebas y deuda TODO-01/TODO-02; ADR con alternativas de ámbito de instrucciones y su verificación |

**Nota transversal:** toda evidencia que incluya salida de Copilot se acompaña de la frase "la salida real variará" y de la verificación aplicada, con el comando ejecutado y su resultado. Una captura de Copilot sin verificación humana no se acepta como evidencia.
