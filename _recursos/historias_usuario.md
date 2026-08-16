# Historias de usuario del repositorio de práctica

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio:** `copilot-lab-catalogo` (`05_codigo_laboratorios/inicial/`)
**Requisito contractual cubierto:** R-152
**Fecha de verificación técnica:** 2026-08-14

Este documento contiene las cuatro historias de usuario que se trabajan en el curso. Todas se refieren al código real del repositorio de práctica: las rutas, las firmas y los defectos citados existen y han sido verificados leyendo el código.

**Cómo se usa este documento.** El participante lo lee antes del laboratorio correspondiente y lo utiliza como fuente para construir sus prompts. La regla pedagógica del curso es que **los criterios de aceptación se escriben antes de pedirle nada a Copilot**: sin criterios previos no hay forma de evaluar la salida del modelo, y el participante acaba aceptando lo primero que aparece.

| Historia | Corresponde a | Jornada | Laboratorio | Estado en `inicial/` |
|---|---|---|---|---|
| **HU-01** Actualización de precio con reglas de descuento | TODO-01 | 3 | LAB-05 (y pruebas en LAB-07) | El endpoint no existe; `ActualizarPrecioAsync` lanza `NotImplementedException` |
| **HU-02** Búsqueda y filtrado de productos | TODO-02 | 3 | Variación opcional avanzada | `BuscarAsync` lanza `NotImplementedException`; `GET /api/productos` devuelve 500 |
| **HU-03** Validación de la creación de productos | BUG-02 | 4 | LAB-08 | `POST /api/productos` acepta datos inválidos y devuelve 201 |
| **HU-04** Gestión segura de la credencial del servicio de precios heredado | BUG-03 | 4 | LAB-08 | Clave simulada en el código, en `appsettings.json` y escrita en el log |

> **Aviso sobre el secreto del repositorio.** El valor `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000` que aparece en HU-04 es **deliberadamente falso**, no da acceso a nada y forma parte del ejercicio de seguridad. No debe sustituirse por una credencial real en ningún momento del curso.

---

## HU-01 — Actualización de precio con reglas de descuento

> **Como** administrador del catálogo
> **quiero** actualizar el precio de un producto aplicando las reglas de descuento vigentes
> **para** mantener los precios alineados con la política comercial.

### Contexto de negocio

El área comercial negocia precios por operación: un mismo producto se cotiza distinto según el volumen de la compra y según si el cliente pertenece al programa de clientes preferentes. Hoy ese cálculo se hace en una hoja de cálculo que cada representante mantiene por su cuenta, lo que produce precios distintos para el mismo escenario y discusiones de facturación cada cierre de mes.

La decisión tomada es centralizar la regla en la API del catálogo, de modo que exista **una sola implementación** de la política comercial y que cualquier canal (portal, integración con el sistema legado, herramientas internas) obtenga el mismo número. La lógica de cálculo ya está aislada en `IDescuentoCalculator`; lo que falta es exponerla como operación de la API y persistir el resultado.

**Alcance de esta historia.** Se trata del precio unitario resultante de aplicar la política, no del importe total de una orden. `IDescuentoCalculator.CalcularPrecioFinal` devuelve el **precio unitario final**, no el importe de la línea. Multiplicar por la cantidad es responsabilidad de quien construya la orden, y queda fuera de esta historia.

### Reglas de descuento en detalle

La política comercial vigente, tal como está declarada en `README.md` y en `.github/copilot-instructions.md` del repositorio:

| Regla | Condición | Descuento |
|---|---|---|
| R1 Volumen bajo | `cantidad >= 10` y `cantidad < 50` | 5 % |
| R2 Volumen medio | `cantidad >= 50` y `cantidad < 100` | 10 % |
| R3 Volumen alto | `cantidad >= 100` | 15 % |
| R4 Cliente preferente | `esClientePreferente == true` | 8 % adicional |
| R5 Composición | El descuento por volumen y el de cliente preferente **se suman**, no se componen | — |
| R6 Tope | El descuento total resultante **se topa en 20 %** | — |
| R7 Validación | `precioBase > 0` y `cantidad >= 1`; en caso contrario `ArgumentOutOfRangeException` | — |
| R8 Redondeo | El resultado se redondea a 2 decimales con `MidpointRounding.AwayFromZero` | — |

Cuatro precisiones que en clase generan preguntas todos los años:

1. **Los umbrales son inclusivos.** `cantidad = 10` ya obtiene el 5 %. Este es exactamente el punto donde falla BUG-01, que usa `>` en lugar de `>=`.
2. **Los descuentos se suman, no se componen.** Un cliente preferente que compra 50 unidades obtiene 18 %, no `1 - (0,90 × 0,92) = 17,2 %`. La diferencia es pequeña en porcentaje y grande en discusiones de facturación.
3. **El tope se aplica después de sumar**, nunca antes ni a cada componente por separado. La suma teórica máxima es 23 % (15 % + 8 %) y siempre se recorta a 20 %.
4. **El redondeo es `AwayFromZero`, no el de banca.** `Math.Round(valor, 2, MidpointRounding.AwayFromZero)`. El comportamiento por defecto de `Math.Round` en .NET es `ToEven`, y produce resultados distintos en los casos que caen exactamente en la mitad. Los ejemplos E-04 y E-11 de la tabla siguiente están elegidos precisamente para exponer esa diferencia.

### Ejemplos numéricos de entrada a salida

Cálculo verificado a mano aplicando R1 a R8 en orden. La columna "Descuento aplicado" muestra la suma antes del tope y, entre paréntesis, el valor efectivo tras aplicarlo.

| # | `precioBase` | `cantidad` | `esClientePreferente` | Descuento aplicado | Cálculo | Precio final |
|---|---|---|---|---|---|---|
| E-01 | 100.00 | 9 | false | 0 % | `100,00 × 1,00` | **100.00** |
| E-02 | 100.00 | 10 | false | 5 % | `100,00 × 0,95` | **95.00** |
| E-03 | 100.00 | 49 | false | 5 % | `100,00 × 0,95` | **95.00** |
| E-04 | 249.50 | 49 | false | 5 % | `249,50 × 0,95 = 237,025` → `AwayFromZero` | **237.03** |
| E-05 | 249.50 | 50 | false | 10 % | `249,50 × 0,90` | **224.55** |
| E-06 | 549.00 | 99 | false | 10 % | `549,00 × 0,90` | **494.10** |
| E-07 | 549.00 | 100 | false | 15 % | `549,00 × 0,85` | **466.65** |
| E-08 | 100.00 | 5 | true | 8 % | `100,00 × 0,92` | **92.00** |
| E-09 | 89.99 | 10 | true | 13 % (5 + 8) | `89,99 × 0,87 = 78,2913` | **78.29** |
| E-10 | 100.00 | 50 | true | 18 % (10 + 8) | `100,00 × 0,82` | **82.00** |
| E-11 | 129.90 | 10 | false | 5 % | `129,90 × 0,95 = 123,405` → `AwayFromZero` | **123.41** |
| E-12 | 100.00 | 100 | true | 23 % → **topado en 20 %** | `100,00 × 0,80` | **80.00** |
| E-13 | 1250.00 | 120 | true | 23 % → **topado en 20 %** | `1.250,00 × 0,80` | **1000.00** |
| E-14 | 24.90 | 100 | true | 23 % → **topado en 20 %** | `24,90 × 0,80` | **19.92** |
| E-15 | 899.99 | 12 | false | 5 % | `899,99 × 0,95 = 854,9905` | **854.99** |
| E-16 | 74.50 | 10 | false | 5 % | `74,50 × 0,95 = 70,775` → `AwayFromZero` | **70.78** |
| E-17 | 0.00 | 5 | false | — | Entrada inválida (R7) | **400** con `ProblemDetails` |
| E-18 | -1.00 | 5 | false | — | Entrada inválida (R7) | **400** con `ProblemDetails` |
| E-19 | 100.00 | 0 | false | — | Entrada inválida (R7) | **400** con `ProblemDetails` |
| E-20 | 100.00 | -3 | true | — | Entrada inválida (R7) | **400** con `ProblemDetails` |

**Ejemplos que exponen el redondeo.** En E-04, `AwayFromZero` da 237,03 mientras que el redondeo de banca (`ToEven`, el comportamiento por defecto de `Math.Round`) daría 237,02. En E-11, `AwayFromZero` da 123,41 y `ToEven` daría 123,40. Si Copilot genera `Math.Round(precioFinal, 2)` sin el tercer argumento, estos dos casos lo delatan y ningún otro de la tabla lo hace.

**Ejemplos que exponen BUG-01** (vigente en `inicial/` hasta que se corrige en LAB-06): con el código defectuoso, E-02 devuelve **100,00** en vez de 95,00 (umbral con `>`), y E-12 devuelve **82,00** en vez de 80,00 (sin tope, con `>` en el umbral de 100 el descuento real es 18 %). Con `cantidad = 120` y cliente preferente el descuento defectuoso alcanza el 23 %, que es el caso que el enunciado del curso menciona.

### Criterios de aceptación

| ID | Criterio | Cómo se comprueba |
|---|---|---|
| **CA-1** | Existe el endpoint `PATCH /api/productos/{id}/precio` que recibe `{ precioBase, cantidad, esClientePreferente }` | `curl -X PATCH` devuelve algo distinto de 404 por ruta inexistente; el endpoint aparece en `http://localhost:5080/swagger` |
| **CA-2** | El precio final se calcula con `IDescuentoCalculator` aplicando volumen, cliente preferente, suma de descuentos y tope del 20 % | Inspección: `ProductoService.ActualizarPrecioAsync` invoca `_calculadora.CalcularPrecioFinal(...)` y no reimplementa la regla |
| **CA-3** | Si el producto no existe, la respuesta es **404** con `ProblemDetails` | `PATCH /api/productos/9999/precio` devuelve 404 y un cuerpo con `title`, `detail` y `status` |
| **CA-4** | Si `precioBase <= 0` o `cantidad < 1`, la respuesta es **400** con el detalle del campo inválido | Casos E-17 a E-20; el cuerpo nombra el campo que falló |
| **CA-5** | La respuesta exitosa es **200** con el `ProductoResponse` actualizado, incluido el precio final calculado | Caso E-03: 200 con `precioBase: 95.00` |
| **CA-6** | El precio se persiste: una consulta posterior `GET /api/productos/{id}` devuelve el precio actualizado | Secuencia `PATCH` seguido de `GET` sobre el mismo `id` en el mismo proceso |
| **CA-7** | Existen pruebas unitarias que cubren los umbrales exactos (9/10, 49/50, 99/100), el tope del 20 %, el redondeo y las entradas inválidas | `dotnet test` en verde y presencia de los casos en `tests/Catalogo.Api.Tests/` |
| **CA-8** | La operación queda documentada con XML docs y atributos `[ProducesResponseType]` | La acción declara 200, 400 y 404; Swagger publica la descripción |

### Casos de prueba sugeridos

Sobre `DescuentoCalculator` (pruebas unitarias puras, sin dependencias):

| Prueba | Entrada | Resultado esperado | Qué protege |
|---|---|---|---|
| `CalcularPrecioFinal_CantidadNueve_NoAplicaDescuento` | 100.00, 9, false | 100.00 | Límite inferior de R1 |
| `CalcularPrecioFinal_CantidadDiez_AplicaCincoPorCiento` | 100.00, 10, false | 95.00 | **Umbral exacto de R1. Falla con BUG-01** |
| `CalcularPrecioFinal_CantidadCuarentaYNueve_AplicaCincoPorCiento` | 100.00, 49, false | 95.00 | Límite superior de R1 |
| `CalcularPrecioFinal_CantidadCincuenta_AplicaDiezPorCiento` | 100.00, 50, false | 90.00 | **Umbral exacto de R2. Falla con BUG-01** |
| `CalcularPrecioFinal_CantidadNoventaYNueve_AplicaDiezPorCiento` | 100.00, 99, false | 90.00 | Límite superior de R2 |
| `CalcularPrecioFinal_CantidadCien_AplicaQuincePorCiento` | 100.00, 100, false | 85.00 | **Umbral exacto de R3. Falla con BUG-01** |
| `CalcularPrecioFinal_ClientePreferenteSinVolumen_AplicaOchoPorCiento` | 100.00, 5, true | 92.00 | R4 aislada |
| `CalcularPrecioFinal_VolumenMedioYPreferente_SumaLosDescuentos` | 100.00, 50, true | 82.00 | **R5: suma, no composición.** Con composición daría 82,80 |
| `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` | 100.00, 100, true | 80.00 | **R6. Ya existe en el repositorio con `Skip`; se habilita en LAB-06** |
| `CalcularPrecioFinal_RedondeoAlza_UsaAwayFromZero` | 249.50, 49, false | 237.03 | **R8. Falla si se omite `MidpointRounding.AwayFromZero`** |
| `CalcularPrecioFinal_RedondeoSegundoCaso_UsaAwayFromZero` | 129.90, 10, false | 123.41 | R8, segundo testigo |
| `CalcularPrecioFinal_ArgumentosInvalidos_LanzaArgumentOutOfRangeException` (`[Theory]`) | (0, 1), (-1, 1), (100, 0), (100, -3) | `ArgumentOutOfRangeException` | R7 |

Sobre `ProductoService.ActualizarPrecioAsync`:

| Prueba | Escenario | Resultado esperado |
|---|---|---|
| `ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado` | `id = 1`, 100.00, 11, false | `PrecioBase == 95.00`. **Ya existe con `Skip` en `ProductoServiceTests.cs`.** Usa `Cantidad = 11` a propósito, para que pase en cuanto se implemente TODO-01 sin depender de que BUG-01 esté corregido |
| `ActualizarPrecioAsync_ProductoInexistente_DevuelveNull` | `id = 9999` | `null` (el controller lo traduce a 404) |
| `ActualizarPrecioAsync_ProductoExistente_PersisteElPrecio` | `PATCH` y luego `ObtenerPorIdAsync` | El segundo lee el precio nuevo |
| `ActualizarPrecioAsync_PrecioBaseCero_PropagaArgumentOutOfRangeException` | `precioBase = 0` | `ArgumentOutOfRangeException` (o `ValidationException`, según la decisión de diseño; documentar cuál) |

> **Nota sobre la prueba con `Skip`.** El atributo del repositorio dice literalmente `[Fact(Skip = "Habilitar en el Laboratorio 4 (HU-01)")]`. Según el mapa de laboratorios del brief, HU-01 se implementa en **LAB-05** (jornada 3). El texto del `Skip` está desactualizado; el laboratorio correcto es LAB-05. El instructor debe anunciarlo para evitar confusión.

### Tareas técnicas derivadas

| # | Tarea | Archivo | Estimación relativa |
|---|---|---|---|
| T-1 | Implementar `ActualizarPrecioAsync`: leer el producto, calcular con `IDescuentoCalculator`, asignar `PrecioBase`, persistir con `ActualizarAsync`, devolver `ProductoResponse` o `null` | `src/Catalogo.Api/Services/ProductoService.cs` (línea 71) | Media |
| T-2 | Añadir la acción `PATCH` con `[HttpPatch("{id:int}/precio")]`, los tres `[ProducesResponseType]` y la traducción a 404 | `src/Catalogo.Api/Controllers/ProductosController.cs` (línea 90) | Baja |
| T-3 | Decidir dónde se valida `precioBase` y `cantidad`: DataAnnotations en `ActualizarPrecioRequest`, validación explícita en el servicio, o ambas. Documentar la decisión | `src/Catalogo.Api/Dtos/ActualizarPrecioRequest.cs` | Baja |
| T-4 | Traducir `ArgumentOutOfRangeException` a una respuesta 400 con `ProblemDetails` que nombre el campo | Controller o middleware (TODO-03) | Media |
| T-5 | Escribir las pruebas de CA-7 y habilitar la prueba con `Skip` de `ProductoServiceTests` | `tests/Catalogo.Api.Tests/` | Media |
| T-6 | Documentación XML de la acción y del método del servicio | Controller, `IProductoService` | Baja |

**Dependencia con BUG-01.** T-1 no requiere que BUG-01 esté corregido. Si el participante implementa HU-01 con el `DescuentoCalculator` defectuoso, la prueba con `Cantidad = 11` pasa igualmente, pero las pruebas de umbral de CA-7 fallarán. Esa es la secuencia pedagógica prevista: HU-01 en LAB-05, corrección de BUG-01 en LAB-06.

### Definición de terminado

La historia está terminada cuando **todo** lo siguiente es cierto y verificable por un tercero:

- [ ] Los ocho criterios CA-1 a CA-8 se cumplen y cada uno se ha comprobado con el comando o la inspección indicados.
- [ ] `dotnet build CopilotLabCatalogo.sln` termina con **0 errores y 0 warnings**.
- [ ] `dotnet test CopilotLabCatalogo.sln` termina en verde, con las pruebas nuevas incluidas y sin haber modificado ni eliminado pruebas existentes.
- [ ] Los ejemplos E-01 a E-16 de este documento se reproducen con el resultado indicado (al menos cuatro de ellos ejecutados contra la API en marcha, no solo en pruebas unitarias).
- [ ] El código respeta `.github/copilot-instructions.md` y `.github/instructions/*.instructions.md`: capas respetadas, sufijo `Async`, `CancellationToken` propagado, `decimal` para importes, sin `Console.WriteLine`, DTO inmutables.
- [ ] Existe un Pull Request abierto con la plantilla del repositorio completa, incluida la tabla de decisiones sobre sugerencias de Copilot.
- [ ] Se ha solicitado Copilot code review y cada comentario recibido está clasificado (accionable ahora, accionable después, informativo o incorrecto) con una respuesta escrita.
- [ ] La bitácora de decisiones registra al menos dos entradas asociadas a esta historia, con motivo explícito y método de verificación.

---

## HU-02 — Búsqueda y filtrado de productos

> **Como** operador del catálogo
> **quiero** buscar productos combinando categoría, rango de precio y texto del nombre, con resultados paginados
> **para** localizar rápidamente un producto sin descargar el catálogo completo.

**Corresponde a TODO-02.** Es la **variación opcional avanzada** de la jornada 3: no es necesaria para superar los criterios de aceptación de LAB-05, y está pensada para quien termine HU-01 antes de tiempo.

### Contexto de negocio

`GET /api/productos` es el punto de entrada más usado del catálogo, y hoy **devuelve 500** porque `ProductoService.BuscarAsync` lanza `NotImplementedException` (línea 39 del servicio). El contrato de entrada ya está definido: existe el DTO `BusquedaProductosQuery` con todos los filtros, las constantes `TamanoPaginaPredeterminado = 20` y `TamanoPaginaMaximo = 100`, y el controller ya lo recibe con `[FromQuery]`. Lo que falta es exclusivamente la implementación del filtrado y la paginación en el servicio.

Esta historia es interesante para el curso porque tiene **muchas combinaciones y pocas líneas de código**: es el caso típico en el que Copilot produce una implementación plausible y compilable que falla en los bordes (paginación con `pagina = 0`, `tamanoPagina` mayor que el tope, comparación de texto sensible a mayúsculas, orden no determinista).

### Contrato

`GET /api/productos` con estos parámetros de query, todos opcionales y combinables con AND:

| Parámetro | Tipo | Obligatorio | Valor por defecto | Restricciones |
|---|---|---|---|---|
| `categoria` | `Categoria?` (`Electronica`, `Hogar`, `Oficina`, `Consumible`) | No | Sin filtro | Debe ser un valor válido del enum |
| `precioMinimo` | `decimal?` | No | Sin cota inferior | Cota **inclusiva**; no negativo |
| `precioMaximo` | `decimal?` | No | Sin cota superior | Cota **inclusiva**; debe ser `>= precioMinimo` si ambos vienen |
| `texto` | `string?` | No | Sin filtro | Coincidencia parcial en `Nombre`, **sin distinguir mayúsculas** |
| `pagina` | `int` | No | **1** | Mínimo 1 |
| `tamanoPagina` | `int` | No | **20** | Mínimo 1, **máximo 100** |

Respuestas: **200** con la página de `ProductoResponse`; **400** con `ProblemDetails` si los parámetros son inconsistentes.

### Criterios de aceptación

| ID | Criterio |
|---|---|
| **CA-1** | `GET /api/productos` sin parámetros devuelve **200** con los 12 productos de la semilla (cabe en la primera página de 20) |
| **CA-2** | `GET /api/productos?categoria=Oficina` devuelve exactamente los productos 8, 9 y 10 |
| **CA-3** | `GET /api/productos?precioMinimo=100&precioMaximo=600` devuelve solo productos cuyo `PrecioBase` está en el intervalo **inclusivo** `[100, 600]`: los identificadores 2 (249.50), 3 (129.90), 4 (549.00), 6 (199.00) y 10 (459.00), cinco elementos |
| **CA-4** | `GET /api/productos?texto=ESCRITORIO` devuelve los productos 5 ("Lampara de escritorio LED") y 9 ("Escritorio elevable manual"): la comparación **no distingue mayúsculas** |
| **CA-5** | Los filtros son **combinables**: `?categoria=Electronica&precioMinimo=200&texto=teclado` devuelve solo el producto 2 |
| **CA-6** | La paginación funciona: `?tamanoPagina=5&pagina=2` devuelve los elementos 6 a 10 del conjunto filtrado, en un orden **estable y determinista** (por `Id` ascendente) |
| **CA-7** | Los valores por defecto se aplican cuando el parámetro no viene: `pagina = 1`, `tamanoPagina = 20` |
| **CA-8** | `tamanoPagina` mayor que **100** se comporta según la regla declarada y documentada: se recorta a 100 o se rechaza con 400. La decisión se toma explícitamente y se documenta; lo que no se acepta es un comportamiento no declarado |
| **CA-9** | `pagina` menor que 1 o `tamanoPagina` menor que 1 producen **400** con `ProblemDetails`, o se normalizan al valor por defecto según la regla declarada |
| **CA-10** | `precioMinimo > precioMaximo` produce **400** con `ProblemDetails` que nombra el conflicto |
| **CA-11** | Una página fuera del rango de resultados devuelve **200** con una lista vacía, no 404 ni error |
| **CA-12** | El endpoint deja de devolver 500: `BuscarAsync` ya no lanza `NotImplementedException` |
| **CA-13** | Existen pruebas unitarias de `BuscarAsync` que cubren cada filtro por separado, la combinación de filtros, los bordes de la paginación y los parámetros inválidos |
| **CA-14** | La operación está documentada con XML docs y `[ProducesResponseType]` para 200 y 400 |

### Casos de prueba sugeridos

| Prueba | Escenario | Resultado esperado |
|---|---|---|
| `BuscarAsync_SinFiltros_DevuelveTodaLaSemilla` | Consulta vacía | 12 elementos |
| `BuscarAsync_PorCategoria_DevuelveSoloEsaCategoria` (`[Theory]`) | `Electronica` / `Hogar` / `Oficina` / `Consumible` | 4 / 3 / 3 / 2 elementos |
| `BuscarAsync_PrecioMinimoInclusivo_IncluyeElValorExacto` | `precioMinimo = 89.99` | Incluye el producto 5, cuyo precio es exactamente 89.99 |
| `BuscarAsync_PrecioMaximoInclusivo_IncluyeElValorExacto` | `precioMaximo = 24.90` | Incluye el producto 11 |
| `BuscarAsync_TextoEnMayusculas_EncuentraIgual` | `texto = "MOUSE"` | Encuentra "Mouse inalambrico ergonomico" |
| `BuscarAsync_TextoParcial_CoincideEnMedioDelNombre` | `texto = "papel"` | Encuentra "Resma de papel A4 500 hojas" |
| `BuscarAsync_TextoSinCoincidencias_DevuelveListaVacia` | `texto = "zzzz"` | Lista vacía, no `null` |
| `BuscarAsync_FiltrosCombinados_AplicaTodosLosCriterios` | Categoría + precio + texto | Un solo elemento |
| `BuscarAsync_SegundaPagina_DevuelveElBloqueCorrecto` | `pagina = 2`, `tamanoPagina = 5` | Identificadores 6 a 10 |
| `BuscarAsync_PaginaFueraDeRango_DevuelveListaVacia` | `pagina = 99` | Lista vacía y 200 |
| `BuscarAsync_TamanoPaginaSuperiorAlTope_SeRecortaACien` | `tamanoPagina = 500` | Como máximo 100 elementos (o 400, según la regla declarada en CA-8) |
| `BuscarAsync_ParametrosInvalidos_LanzaValidationException` (`[Theory]`) | `pagina = 0`, `tamanoPagina = 0`, `precioMinimo > precioMaximo` | Excepción de validación |
| `BuscarAsync_ProductoInactivo_SeIncluyeSalvoQueSeDeclareLoContrario` | Producto 10 tiene `Activo = false` | Documentar la decisión: la semilla incluye un producto inactivo precisamente para forzar esta pregunta |

> **La pregunta que Copilot casi nunca hace.** El producto 10 ("Archivador metalico de tres cajones") tiene `Activo = false`. Ni el contrato ni el DTO dicen qué debe hacer la búsqueda con los productos inactivos. Copilot elegirá una de las dos opciones sin avisar. El participante debe detectar la ambigüedad, decidir, documentar la decisión y escribir la prueba que la fija. Este es el punto pedagógico más valioso de HU-02.

### Tareas técnicas derivadas

| # | Tarea | Archivo |
|---|---|---|
| T-1 | Implementar el filtrado combinable sobre el resultado de `ObtenerTodosAsync` | `Services/ProductoService.cs` (línea 39) |
| T-2 | Normalizar `Pagina` y `TamanoPagina` con las constantes `TamanoPaginaPredeterminado` y `TamanoPaginaMaximo` ya declaradas en `BusquedaProductosQuery` | `Services/ProductoService.cs` |
| T-3 | Fijar un orden determinista antes de paginar (por `Id` ascendente) | `Services/ProductoService.cs` |
| T-4 | Validar la consistencia de los parámetros y traducirla a 400 con `ProblemDetails` | Servicio + controller o middleware |
| T-5 | Decidir y documentar el tratamiento de los productos con `Activo = false` | Documentación XML y ADR breve |
| T-6 | Comparación de texto sin distinguir mayúsculas y culturalmente explícita (`StringComparison.OrdinalIgnoreCase`), no `ToLower()` sobre cada elemento | `Services/ProductoService.cs` |
| T-7 | Pruebas de CA-13 | `tests/Catalogo.Api.Tests/ProductoServiceTests.cs` |

### Definición de terminado

- [ ] CA-1 a CA-14 comprobados.
- [ ] `GET /api/productos` ya no devuelve 500 en ninguna combinación de parámetros válidos.
- [ ] Las decisiones ambiguas (productos inactivos, comportamiento con `tamanoPagina > 100`, `pagina < 1`) están **documentadas**, no solo implementadas.
- [ ] `dotnet build` sin warnings y `dotnet test` en verde.
- [ ] La bitácora registra al menos una decisión de tipo "modificado" o "descartado" sobre la propuesta de Copilot para el filtrado o la paginación.

---

## HU-03 — Validación de la creación de productos

> **Como** responsable de la calidad del catálogo
> **quiero** que la API rechace los productos con datos inválidos en el momento de crearlos
> **para** que el catálogo no acumule registros corruptos que después haya que depurar a mano.

**Corresponde a BUG-02.** Se descubre y se corrige en la jornada 4 (LAB-08).

### Contexto de negocio

El catálogo alimenta el portal público y el sistema de facturación. Un producto con nombre vacío aparece como una fila en blanco en el portal; un producto con precio negativo genera notas de crédito automáticas en facturación. Ambos casos han ocurrido y el coste no está en el error en sí, sino en el trabajo de localizar y corregir los registros ya propagados a los sistemas aguas abajo.

### Estado actual verificado (el defecto)

`POST /api/productos` con este cuerpo devuelve **201 Created** y persiste el producto inválido:

```json
{ "nombre": "", "categoria": 1, "precioBase": -99, "existencias": -5 }
```

La causa tiene dos partes independientes:

1. `src/Catalogo.Api/Dtos/CrearProductoRequest.cs` no declara **ninguna** DataAnnotation. El DTO es un `record` con cuatro propiedades `init` sin restricción alguna.
2. `src/Catalogo.Api/Services/ProductoService.cs`, método `CrearAsync` (líneas 52 a 68), construye el `Producto` y llama a `AgregarAsync` sin validar nada.

Esto contradice directamente la regla 6 de `.github/copilot-instructions.md` del propio repositorio: *"Toda entrada publica se valida (DataAnnotations en los DTO y validacion explicita en el servicio). Los datos invalidos producen un error 400 con `ProblemDetails`."* El defecto es, por tanto, una violación de una convención declarada, no una omisión de diseño: es exactamente el tipo de hallazgo que se espera de una revisión de seguridad.

### Reglas de validación exigidas

| Campo | Regla | Anotación sugerida |
|---|---|---|
| `Nombre` | Obligatorio, entre 3 y 100 caracteres, **no puede estar formado solo por espacios** | `[Required]`, `[StringLength(100, MinimumLength = 3)]` más una comprobación explícita de `string.IsNullOrWhiteSpace` |
| `Categoria` | Debe ser un valor definido del enum `Categoria` (0 a 3) | `[EnumDataType(typeof(Categoria))]` |
| `PrecioBase` | Mayor que 0 y dentro de un máximo razonable | `[Range(0.01, 1000000)]` |
| `Existencias` | Mayor o igual que 0 | `[Range(0, int.MaxValue)]` |

> **Punto crítico que las DataAnnotations no cubren.** `[Required]` y `[StringLength(100, MinimumLength = 3)]` **aceptan** el nombre `"   "` (tres espacios), porque tiene longitud 3 y no es nulo. La validación de espacios en blanco debe añadirse explícitamente. Cuando se le pide a Copilot que valide el DTO, esta es la omisión que produce con más frecuencia, y es la que el participante debe detectar. Un buen contraste: pedirle a Copilot que escriba primero la **prueba** con `Nombre = "   "` y comprobar si su propia implementación la pasa.

### Criterios de aceptación

| ID | Criterio |
|---|---|
| **CA-1** | `POST /api/productos` con nombre vacío devuelve **400**, no 201 |
| **CA-2** | `POST /api/productos` con `precioBase <= 0` devuelve **400** |
| **CA-3** | `POST /api/productos` con `existencias < 0` devuelve **400** |
| **CA-4** | `POST /api/productos` con un valor de `categoria` fuera del enum devuelve **400** |
| **CA-5** | `POST /api/productos` con `nombre = "   "` (solo espacios) devuelve **400** |
| **CA-6** | El cuerpo de la respuesta 400 es un `ProblemDetails` que **nombra el campo o los campos inválidos**; no es un mensaje genérico ni una traza de excepción |
| **CA-7** | La respuesta de error **no filtra** detalles internos: sin nombres de tipos internos, sin rutas de archivo, sin `StackTrace` |
| **CA-8** | Ninguna solicitud inválida persiste: tras un 400, `GET /api/productos/{id}` sobre el identificador que se habría asignado devuelve 404 |
| **CA-9** | Una solicitud válida sigue devolviendo **201** con `Location` apuntando al recurso creado (`CreatedAtAction`) |
| **CA-10** | La validación se ejecuta también cuando `CrearAsync` se invoca directamente desde el servicio, no solo a través del controller: la regla vive en la capa de negocio y lanza `ValidationException` |
| **CA-11** | Existen pruebas unitarias por cada regla y por cada campo, más al menos una prueba del camino feliz |
| **CA-12** | `dotnet build` sigue sin warnings y ninguna prueba existente fue modificada |

### Casos de prueba sugeridos

| Prueba | Entrada | Resultado esperado |
|---|---|---|
| `CrearAsync_NombreVacio_LanzaValidationException` | `Nombre = ""` | `ValidationException` |
| `CrearAsync_NombreSoloEspacios_LanzaValidationException` | `Nombre = "   "` | `ValidationException`. **Prueba clave** |
| `CrearAsync_NombreDeDosCaracteres_LanzaValidationException` | `Nombre = "ab"` | `ValidationException` (borde inferior de longitud) |
| `CrearAsync_NombreDeTresCaracteres_CreaElProducto` | `Nombre = "abc"` | Producto creado (borde inferior válido) |
| `CrearAsync_NombreDeCienCaracteres_CreaElProducto` | 100 caracteres | Producto creado (borde superior válido) |
| `CrearAsync_NombreDeCientoUnCaracteres_LanzaValidationException` | 101 caracteres | `ValidationException` |
| `CrearAsync_PrecioNegativo_LanzaValidationException` (`[Theory]`) | `-99`, `-0.01`, `0` | `ValidationException` |
| `CrearAsync_PrecioMinimoValido_CreaElProducto` | `0.01` | Producto creado |
| `CrearAsync_ExistenciasNegativas_LanzaValidationException` | `-5` | `ValidationException` |
| `CrearAsync_ExistenciasCero_CreaElProducto` | `0` | Producto creado (cero es válido: producto agotado) |
| `CrearAsync_CategoriaFueraDelEnum_LanzaValidationException` | `(Categoria)99` | `ValidationException` |
| `CrearAsync_SolicitudValida_AsignaElSiguienteIdentificador` | Datos válidos | Ya existe en el repositorio: espera `Id == 13` |
| `CrearAsync_SolicitudInvalida_NoPersisteNada` | Datos inválidos | El repositorio conserva 12 productos |

### Tareas técnicas derivadas

| # | Tarea | Archivo |
|---|---|---|
| T-1 | Añadir las DataAnnotations al DTO | `Dtos/CrearProductoRequest.cs` |
| T-2 | Validar en el servicio con `Validator.TryValidateObject(..., validateAllProperties: true)` y lanzar `ValidationException` | `Services/ProductoService.cs` (línea 52) |
| T-3 | Añadir la comprobación explícita de nombre formado solo por espacios | `Services/ProductoService.cs` |
| T-4 | Traducir `ValidationException` a `ProblemDetails` 400 en el middleware de errores (TODO-03) | `Infrastructure/ManejoErroresMiddleware.cs` |
| T-5 | Verificar que el mensaje de error no filtra información interna | Revisión manual del cuerpo de la respuesta |
| T-6 | Pruebas de CA-11 | `tests/Catalogo.Api.Tests/ProductoServiceTests.cs` |

> **Advertencia sobre `validateAllProperties`.** `Validator.TryValidateObject(objeto, contexto, resultados)` sin el cuarto argumento **solo valida los atributos `[Required]`** e ignora `[Range]`, `[StringLength]` y `[EnumDataType]`. Es un error clásico y Copilot lo reproduce con frecuencia porque abunda en el código público. La prueba `CrearAsync_PrecioNegativo_LanzaValidationException` lo detecta.

### Definición de terminado

- [ ] CA-1 a CA-12 comprobados, incluidos los cinco casos de 400 ejecutados contra la API en marcha.
- [ ] La regla 6 de `.github/copilot-instructions.md` se cumple de forma verificable.
- [ ] El hallazgo está documentado como una **nota de causa raíz** (plantilla de `plantillas_documentacion.md`), no solo corregido.
- [ ] El PR declara explícitamente en la sección de riesgos que se ha endurecido un contrato de entrada, e indica el impacto en los clientes que hoy envían datos inválidos.

---

## HU-04 — Gestión segura de la credencial del servicio de precios heredado

> **Como** responsable de seguridad de la aplicación
> **quiero** que la credencial del servicio de precios heredado se lea de una fuente de configuración segura y no aparezca en el código, en el repositorio ni en los registros
> **para** que la rotación de la credencial no exija un despliegue y para que una filtración de logs no comprometa el sistema heredado.

**Corresponde a BUG-03.** Se descubre y se corrige en la jornada 4 (LAB-08).

> **Recordatorio obligatorio antes de empezar.** El valor `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000` es **falso por diseño**, no da acceso a nada y existe únicamente para este ejercicio. El instructor debe decirlo en voz alta al abrir la jornada 4. No debe sustituirse por una credencial real bajo ninguna circunstancia.

### Contexto de negocio

`LegacyPricingClient` consulta el precio de referencia que publica el sistema heredado de precios. La llamada de red está simulada, pero el patrón de gestión de la credencial es el que se usaría en producción, y es incorrecto en tres puntos a la vez.

En un repositorio real, un secreto escrito en el código queda en el **historial de Git para siempre**: borrarlo en un commit posterior no lo elimina, solo lo oculta a quien mire el estado actual. La rotación de una credencial filtrada exige, además del cambio de la credencial, una reescritura del historial o la asunción de que el valor antiguo es público.

### Estado actual verificado (el defecto)

Tres hallazgos independientes en el mismo flujo:

| # | Hallazgo | Ubicación exacta | Severidad |
|---|---|---|---|
| H-1 | Credencial escrita como constante en el código fuente | `src/Catalogo.Api/Infrastructure/LegacyPricingClient.cs`, línea 9 (`private const string ApiKey = ...`) | Alta |
| H-2 | La misma credencial repetida en un archivo de configuración **versionado** | `src/Catalogo.Api/appsettings.json`, sección `LegacyPricing` | Alta |
| H-3 | La credencial completa se **escribe en el log**: `ObtenerPrecioReferenciaAsync` construye la cadena `"... \| Authorization: Bearer {ApiKey}"` y la pasa a `_logger.LogDebug` | `LegacyPricingClient.cs`, líneas 35 a 37 | Alta |

H-3 es el más fácil de pasar por alto: la revisión se detiene en la constante de la línea 9 y da el trabajo por hecho. Un secreto en un log agregado suele tener **más alcance** que uno en el código, porque los sistemas de observabilidad se comparten entre equipos y con proveedores, y sus retenciones y controles de acceso son distintos de los del repositorio.

Además, la clase declara `BaseUrl` como constante, lo que impide apuntar a un entorno distinto sin recompilar. No es un problema de seguridad, pero pertenece a la misma corrección.

### Criterios de aceptación

| ID | Criterio |
|---|---|
| **CA-1** | La credencial **no aparece** en ningún archivo de código fuente: `git grep -n "sk-FAKE"` sobre `src/` no devuelve coincidencias en archivos `.cs` |
| **CA-2** | La credencial **no aparece** en `appsettings.json` ni en `appsettings.Development.json`; solo permanece `LegacyPricing:BaseUrl` |
| **CA-3** | La credencial se lee de `IConfiguration` con la clave `LegacyPricing:ApiKey`, alimentable por variable de entorno `LegacyPricing__ApiKey` o por `dotnet user-secrets` |
| **CA-4** | El valor de la credencial **nunca** se escribe en el log, en ningún nivel, ni completo ni como parte de una cadena mayor |
| **CA-5** | Si la credencial no está configurada, el arranque **no falla en silencio**: se registra un warning con `ILogger` que indica qué clave falta y cómo configurarla |
| **CA-6** | Si se intenta usar el cliente sin credencial configurada, la operación lanza `InvalidOperationException` con un mensaje que **no incluye** ningún valor de credencial |
| **CA-7** | `BaseUrl` también se lee de configuración, con un valor por defecto documentado |
| **CA-8** | El `.gitignore` cubre los archivos que pudieran contener secretos locales (`appsettings.*.local.json`, `*.user`, `.env`) |
| **CA-9** | El `README.md` o la documentación del módulo explica **cómo configurar la credencial en local** con el comando exacto de `dotnet user-secrets` |
| **CA-10** | Existen pruebas que verifican el comportamiento con credencial ausente y con credencial presente, **sin usar ninguna credencial real** |
| **CA-11** | El PR declara explícitamente el hallazgo en la sección de riesgos e indica que, en un repositorio real, la credencial tendría que **rotarse** además de retirarse del código |
| **CA-12** | El informe de la revisión de seguridad **no reproduce el valor completo** del secreto: cita el archivo y la línea |

### Casos de prueba sugeridos

| Prueba | Escenario | Resultado esperado |
|---|---|---|
| `ObtenerPrecioReferenciaAsync_SinCredencialConfigurada_LanzaInvalidOperationException` | `IConfiguration` sin `LegacyPricing:ApiKey` | `InvalidOperationException` |
| `Constructor_SinCredencialConfigurada_RegistraUnWarning` | Igual, con un `ILogger` falso que captura las entradas | Se registra exactamente un warning |
| `ObtenerPrecioReferenciaAsync_ConCredencial_NoRegistraLaCredencialEnElLog` | Credencial de prueba `"clave-de-prueba"`, `ILogger` falso que captura todas las entradas | **Ninguna** entrada de log contiene la subcadena `"clave-de-prueba"`. Prueba central de la historia |
| `ObtenerPrecioReferenciaAsync_ConCredencial_DevuelveElPrecioSimulado` | `productoId = 1` | El precio simulado documentado |
| `ObtenerPrecioReferenciaAsync_BaseUrlDeConfiguracion_UsaElValorConfigurado` | `BaseUrl` alternativa | La llamada usa el valor configurado |

> **Cómo se escribe la prueba de CA-4 sin secretos.** Se inyecta un `ILogger<LegacyPricingClient>` falso que acumula los mensajes formateados en una lista, se ejecuta la operación con una credencial de prueba evidentemente ficticia, y se afirma que ninguna entrada capturada contiene esa cadena. La prueba no necesita ninguna credencial real y protege contra la reintroducción del defecto en el futuro, que es exactamente lo que debe hacer una prueba de regresión de seguridad.

### Tareas técnicas derivadas

| # | Tarea | Archivo |
|---|---|---|
| T-1 | Inyectar `IConfiguration` (o un `IOptions<LegacyPricingOptions>` tipado) en el constructor y eliminar las dos constantes | `Infrastructure/LegacyPricingClient.cs` |
| T-2 | Retirar `ApiKey` de `appsettings.json` conservando `BaseUrl` | `src/Catalogo.Api/appsettings.json` |
| T-3 | Eliminar la credencial de la cadena que se pasa a `LogDebug`; registrar el destino de la llamada sin la cabecera de autorización | `Infrastructure/LegacyPricingClient.cs`, líneas 35 a 37 |
| T-4 | Registrar el warning de credencial ausente y lanzar `InvalidOperationException` en el uso | `Infrastructure/LegacyPricingClient.cs` |
| T-5 | Documentar en `README.md` el comando `dotnet user-secrets set "LegacyPricing:ApiKey" "<valor>"` y la alternativa con variable de entorno `LegacyPricing__ApiKey` | `README.md` |
| T-6 | Revisar y ampliar `.gitignore` | `.gitignore` |
| T-7 | Pruebas de CA-10 | `tests/Catalogo.Api.Tests/` |
| T-8 | Registrar en la bitácora si Copilot detectó los tres hallazgos o solo el primero | Bitácora de decisiones |

**Prompt de partida sugerido.** El prompt file `revisar-seguridad.prompt.md` del repositorio, aplicado sobre `Infrastructure/LegacyPricingClient.cs`, es el punto de entrada previsto. Su instrucción explícita de **no reproducir el secreto completo en la respuesta** es parte del ejercicio: el participante debe comprobar si la salida respeta esa restricción, porque no siempre lo hace.

### Definición de terminado

- [ ] CA-1 a CA-12 comprobados, incluido `git grep` sobre el árbol de trabajo.
- [ ] Los **tres** hallazgos (H-1, H-2, H-3) están corregidos, no solo el de la constante.
- [ ] La aplicación arranca sin credencial configurada y el warning es visible en la consola.
- [ ] Existe una nota de causa raíz que distingue el síntoma (una constante visible) de la causa raíz (no hay ninguna barrera que impida escribir secretos en el código: ni convención aplicada, ni análisis estático, ni revisión con criterio de seguridad).
- [ ] La bitácora registra qué encontró Copilot por sí solo y qué hubo que pedirle explícitamente.

---

## Trazabilidad

| Historia | Requisitos de la propuesta | Resultados de aprendizaje | Laboratorio | Defecto o pendiente |
|---|---|---|---|---|
| HU-01 | R-071, R-072, R-073, R-081, R-082, R-083 | RA-3.3, RA-3.4, RA-4.1, RA-4.2 | LAB-05, LAB-07 | TODO-01 |
| HU-02 | R-071, R-072 (variación opcional) | RA-3.3, RA-3.4 | Variación de LAB-05 | TODO-02 |
| HU-03 | R-086, R-087, R-088 | RA-4.3, RA-4.4 | LAB-08 | BUG-02 |
| HU-04 | R-086, R-087, R-088, R-093 | RA-4.3, RA-4.5, RA-5.5 | LAB-08 | BUG-03 |

## Cómo convertir una historia en prompts

Las cuatro historias comparten el mismo método de trabajo, que es el que se evalúa en el curso:

1. **Leer la historia y sus criterios de aceptación antes de abrir el chat.** Si el participante no sabe qué respuesta espera, no puede juzgar la que reciba.
2. **Descomponer en tareas técnicas** usando el rol de agente `Plan` (o el slash command `/plan`), que investiga y produce un plan sin modificar código `[GA]`. Revisar el plan y corregirlo antes de ejecutarlo.
3. **Aportar contexto explícito**: `#codebase` para la exploración inicial, y luego el archivo o el símbolo concreto. Adjuntar el archivo de instrucciones aplicable cuando la salida no lo esté respetando.
4. **Implementar por partes verificables**, no la historia entera de una vez.
5. **Ejecutar las pruebas después de cada paso**, no al final.
6. **Registrar cada decisión** (aceptado, modificado, descartado) en la bitácora, con motivo y método de verificación.

La biblioteca `06_recursos/biblioteca_prompts.md` contiene los prompts concretos de cada uno de estos pasos, con los identificadores `P-PLA-*`, `P-IMP-*`, `P-TST-*` y `P-SEC-*`.

> **Advertencia constante.** La salida real de Copilot **variará** entre ejecuciones, entre modelos y entre versiones del producto. Los ejemplos de este documento describen lo que debe cumplirse, no lo que el modelo va a responder.
