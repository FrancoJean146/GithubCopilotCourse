# Catálogo de productos - repositorio de práctica (copia INICIAL)

API REST de catálogo de productos en **.NET 8** (ASP.NET Core Web API con controllers).
Es el punto de partida de los laboratorios del curso de GitHub Copilot: el código **compila y las
pruebas pasan**, pero tiene funcionalidad pendiente y algún defecto que tendrás que descubrir.

> **Aviso importante:** el valor `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000` que aparece en este
> repositorio es **falso** y forma parte del ejercicio de seguridad del Día 4. No es una credencial
> real, no da acceso a nada y **no debe reemplazarse por una credencial real** durante el curso.

## Requisitos

- **.NET 8 SDK** (`dotnet --version` debe devolver `8.0.x`).
- Un entorno de desarrollo, a elección:
  - **Visual Studio 2022 17.14 o posterior** (o Visual Studio 2026). Es el mínimo seguro para el
    modo agente y para Copilot code review. La documentación oficial de Microsoft es inconsistente
    en este punto (según la página se cita 17.8, 17.10 o 17.14 como versión mínima), así que el
    curso toma **17.14+** como requisito para disponer de todas las funcionalidades que se usan.
  - **VS Code**: extensión `ms-dotnettools.csharp` **requerida**;
    `ms-dotnettools.csdevkit` **recomendada pero opcional**.
  - **JetBrains Rider**.
- Git.

> **Nota sobre C# Dev Kit:** `ms-dotnettools.csdevkit` se distribuye bajo una licencia propia de
> Microsoft y tu organización puede tenerlo restringido por política. El curso funciona igualmente
> sin él: la compilación y las pruebas se ejecutan por línea de comandos con `dotnet build` y
> `dotnet test`, y `ms-dotnettools.csharp` cubre el resaltado, la navegación y el diagnóstico.

## Cómo compilar

```bash
dotnet restore CopilotLabCatalogo.sln
dotnet build CopilotLabCatalogo.sln --no-restore
```

## Cómo probar

```bash
dotnet test CopilotLabCatalogo.sln
```

Resultado esperado al inicio del curso: `dotnet test` debe informar **15 pruebas superadas y 2
omitidas**. Corresponden a 8 métodos de prueba activos (tres de ellos son `[Theory]` con varios
casos) y 2 métodos marcados con `Skip`.

## Cómo ejecutar

```bash
dotnet run --project src/Catalogo.Api/Catalogo.Api.csproj
```

La aplicación escucha en `http://localhost:5080` (configurado en `appsettings.json`).

- **Swagger UI:** <http://localhost:5080/swagger>
- **Documento OpenAPI:** <http://localhost:5080/swagger/v1/swagger.json>

## Estructura

```text
src/Catalogo.Api/
  Controllers/ProductosController.cs   Endpoints HTTP
  Models/                              Producto, Categoria
  Dtos/                                Contratos de entrada y salida (record inmutables)
  Repositories/                        IProductoRepository + implementacion en memoria (12 productos)
  Services/                            IProductoService, IDescuentoCalculator y sus implementaciones
  Infrastructure/LegacyPricingClient   Cliente simulado del sistema legado de precios
tests/Catalogo.Api.Tests/              Pruebas con xUnit, FluentAssertions y Moq
```

## Endpoints disponibles

| Verbo | Ruta | Descripción | Respuestas |
| --- | --- | --- | --- |
| GET | `/api/productos` | Búsqueda con filtros (`categoria`, `precioMinimo`, `precioMaximo`, `texto`) y paginación (`pagina`, `tamanoPagina`) | 200, 400 |
| GET | `/api/productos/{id}` | Obtiene un producto por identificador | 200, 404 |
| POST | `/api/productos` | Crea un producto | 201, 400 |
| DELETE | `/api/productos/{id}` | Elimina un producto | 204, 404 |

`PATCH /api/productos/{id}/precio` **todavía no existe**: es la historia de usuario HU-01 (TODO-01).

> Nota: `GET /api/productos` responde con error mientras TODO-02 no esté implementado.

## Reglas de negocio del descuento

`IDescuentoCalculator.CalcularPrecioFinal(precioBase, cantidad, esClientePreferente)` devuelve el
**precio unitario final** y debe cumplir:

1. Descuento por volumen: `cantidad >= 10` -> 5 %; `cantidad >= 50` -> 10 %; `cantidad >= 100` -> 15 %.
2. Cliente preferente: 8 % adicional.
3. Los descuentos **se suman** (no se componen) y el total **se topa en 20 %**.
4. `precioBase` debe ser mayor que 0 y `cantidad` mayor o igual que 1; en caso contrario se lanza
   `ArgumentOutOfRangeException`.
5. El resultado se redondea a 2 decimales con `MidpointRounding.AwayFromZero`.

## Funcionalidad pendiente (TODO)

| TODO | Archivo | Qué falta |
| --- | --- | --- |
| **TODO-01** | `src/Catalogo.Api/Controllers/ProductosController.cs` y `src/Catalogo.Api/Services/ProductoService.cs` | Endpoint `PATCH /api/productos/{id}/precio` con `ActualizarPrecioRequest`, que calcula el precio final con `IDescuentoCalculator`, lo persiste y devuelve `ProductoResponse` (200, 400 y 404). |
| **TODO-02** | `src/Catalogo.Api/Services/ProductoService.cs` | `BuscarAsync` lanza `NotImplementedException`: falta filtrar por categoría, rango de precio y texto del nombre (sin distinguir mayúsculas) y paginar (`Pagina` por defecto 1, `TamanoPagina` por defecto 20 y máximo 100). |
| **TODO-03** | `src/Catalogo.Api/Repositories/IProductoRepository.cs`, `src/Catalogo.Api/Services/IProductoService.cs`, `src/Catalogo.Api/Services/IDescuentoCalculator.cs` y `src/Catalogo.Api/Program.cs` | Documentación XML (`///`) de las interfaces públicas y middleware de manejo centralizado de errores que traduzca las excepciones a `ProblemDetails`. |

## Pruebas omitidas: cómo habilitarlas

Dos pruebas están marcadas con `Skip` para que la suite arranque en verde. Para habilitarlas, quita
el argumento `Skip` del atributo `[Fact]`:

```csharp
// Antes
[Fact(Skip = "Habilitar en el Laboratorio 6 (Jornada 3) para exponer BUG-01")]

// Despues
[Fact]
```

| Prueba | Archivo | Cuándo se habilita |
| --- | --- | --- |
| `ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado` | `tests/Catalogo.Api.Tests/ProductoServiceTests.cs` | Laboratorio 5 (Jornada 3), al implementar HU-01 / TODO-01 |
| `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` | `tests/Catalogo.Api.Tests/DescuentoCalculatorTests.cs` | Laboratorio 6 (Jornada 3), al corregir BUG-01 |

Cuando la habilites fallará: eso es lo que se espera. Corrige el **código de producción** hasta que
pase; **no modifiques la prueba**.

## Archivos de configuración de Copilot incluidos

| Archivo | Para qué sirve |
| --- | --- |
| `.github/copilot-instructions.md` | Estándares del repositorio aplicados a todo el proyecto |
| `.github/instructions/*.instructions.md` | Convenciones por tipo de archivo (C#, pruebas, controllers) |
| `.github/prompts/*.prompt.md` | Cinco prompts reutilizables (explicar, generar pruebas, revisar seguridad, documentar, preparar PR) |
| `AGENTS.md` | Instrucciones cross-tool (compilar, probar, reglas de trabajo) |
| `.github/workflows/copilot-setup-steps.yml` | Entorno del agente de codificación de Copilot |
| `.github/pull_request_template.md` | Plantilla de pull request del curso |
| `.vscode/extensions.json` | Extensiones recomendadas (Copilot Chat ya viene integrado en VS Code) |
| `.vscode/mcp.json` | Ejemplo de servidor MCP de GitHub para el Día 5 |
