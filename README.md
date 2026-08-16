# Repositorio de práctica — `copilot-lab-catalogo`

Código de laboratorio del curso **GitHub Copilot para Desarrollo de Software**.

---

## Contenido de esta carpeta

| Elemento | Audiencia | Descripción |
|---|---|---|
| `inicial/` | **Participantes** | Lo que reciben al inicio del curso. Contiene tres funcionalidades incompletas y tres defectos controlados. Es el punto de partida de los diez laboratorios |
| `resuelto/` | **Instructor** | Solución de referencia completa. No se entrega a los participantes antes del cierre del Día 5 |
| `DEFECTOS_INTENCIONALES.md` | **Instructor** | Documento confidencial: ubicación exacta de cada defecto, cómo se manifiesta, cómo se corrige y su valor pedagógico |
| `verificar.sh` / `verificar.ps1` | Instructor y TI | Verifican que ambas copias restauran, compilan y ejecutan sus pruebas |

> **No entregue `resuelto/` ni `DEFECTOS_INTENCIONALES.md` a los participantes antes de tiempo.** El descubrimiento de los defectos es el núcleo de los laboratorios 6 y 8.

---

## El proyecto

Web API en **ASP.NET Core 8** sobre un catálogo de productos. Arquitectura deliberadamente sencilla: controller → servicio → repositorio en memoria, con un calculador de descuentos aislado como objeto de las pruebas.

```
CopilotLabCatalogo.sln
src/Catalogo.Api/
  Controllers/ProductosController.cs
  Models/         Producto.cs · Categoria.cs
  Dtos/           CrearProductoRequest · ActualizarPrecioRequest · ProductoResponse · BusquedaProductosQuery
  Repositories/   IProductoRepository · InMemoryProductoRepository (12 productos de semilla)
  Services/       IProductoService · ProductoService · IDescuentoCalculator · DescuentoCalculator
  Infrastructure/ LegacyPricingClient.cs
tests/Catalogo.Api.Tests/
  DescuentoCalculatorTests.cs · ProductoServiceTests.cs
```

### Endpoints

| Verbo y ruta | Códigos | Estado en `inicial/` |
|---|---|---|
| `GET /api/productos` | 200, 400 | Devuelve **500**: `BuscarAsync` no está implementado (TODO-02) |
| `GET /api/productos/{id:int}` | 200, 404 | Funcional |
| `POST /api/productos` | 201, 400 | Acepta datos inválidos y devuelve **201** (BUG-02) |
| `PATCH /api/productos/{id:int}/precio` | 200, 400, 404 | **No existe** (TODO-01 — es la historia de usuario HU-01) |
| `DELETE /api/productos/{id:int}` | 204, 404 | Funcional |

Parámetros de búsqueda: `categoria`, `precioMinimo`, `precioMaximo`, `texto`, `pagina`, `tamanoPagina`.

### Reglas de negocio del descuento

1. Volumen: `cantidad >= 10` → 5 % · `>= 50` → 10 % · `>= 100` → 15 %
2. Cliente preferente: +8 %
3. Los descuentos **se suman** (no se componen) y el total se **topa en 20 %**
4. `precioBase > 0` y `cantidad >= 1`, si no → `ArgumentOutOfRangeException`
5. Redondeo a 2 decimales con `MidpointRounding.AwayFromZero`

`CalcularPrecioFinal` devuelve el **precio unitario** con el descuento aplicado.

---

## Requisitos

| Requisito | Versión |
|---|---|
| .NET SDK | 8.0 |
| Visual Studio Code | 1.116 o posterior |
| Extensión `ms-dotnettools.csharp` | Requerida |
| Extensión `ms-dotnettools.csdevkit` | Recomendada, opcional (puede estar restringida por política de la organización) |
| Extensión `GitHub.vscode-pull-request-github` | Requerida |
| GitHub Copilot | Asiento activo. **No hay que instalar ninguna extensión de Copilot**: Copilot Chat es built-in desde VS Code 1.116 |

Alternativa: Visual Studio 2026, o Visual Studio 2022 **17.14 o posterior** (agent mode y Copilot code review requieren esa versión).

---

## Puesta en marcha

```bash
cd inicial
dotnet restore
dotnet build
dotnet test
dotnet run --project src/Catalogo.Api
```

Resultado esperado de `dotnet test` en `inicial/`: **15 pruebas superadas y 2 omitidas**. Corresponden a 8 métodos de prueba activos (tres de ellos son `[Theory]` con varios casos) y 2 métodos marcados con `Skip`.

Swagger queda disponible en la URL que imprime `dotnet run` (por defecto `/swagger`).

Para verificar ambas copias de una vez:

```bash
./verificar.sh          # Linux y macOS
```
```powershell
.\verificar.ps1         # Windows
```

---

## Las pruebas omitidas

Las dos pruebas marcadas con `Skip` se habilitan quitando el argumento `Skip` del atributo `[Fact]`:

| Prueba | Se habilita en | Qué expone |
|---|---|---|
| En `ProductoServiceTests.cs` | **Laboratorio 5 (Jornada 3)** | La ausencia de `ActualizarPrecioAsync` (TODO-01 / HU-01) |
| En `DescuentoCalculatorTests.cs` | **Laboratorio 6 (Jornada 3)** | El defecto BUG-01 del cálculo de descuentos |

---

## Aviso sobre la credencial del ejemplo

`src/Catalogo.Api/Infrastructure/LegacyPricingClient.cs` y `appsettings.json` contienen la cadena:

```
sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000
```

**Es un valor falso, deliberadamente evidente, y forma parte del ejercicio del Laboratorio 8.** No es una credencial real, no da acceso a ningún servicio y no debe tratarse como un incidente de seguridad. Su presencia es intencional: el participante debe encontrarla, entender por qué es un defecto y corregirla moviéndola a configuración externa.

Si prefiere no incluir un secreto simulado en su organización, el Laboratorio 8 documenta una variante equivalente sin él.

---

## Configuración de GitHub Copilot incluida

Ambas copias incluyen la configuración que el curso enseña a construir y ajustar:

| Archivo | Propósito |
|---|---|
| `.github/copilot-instructions.md` | Estándares transversales del repositorio |
| `.github/instructions/csharp.instructions.md` | Convenciones de C# (`applyTo: "**/*.cs"`) |
| `.github/instructions/tests.instructions.md` | Convenciones de pruebas (`applyTo: "tests/**/*.cs"`) |
| `.github/instructions/api.instructions.md` | Convenciones de API (`applyTo` a los controllers) |
| `AGENTS.md` | Instrucciones cross-tool: cómo compilar, cómo probar, estructura |
| `.github/prompts/*.prompt.md` | Cinco prompts reutilizables invocables como slash commands |
| `.github/workflows/copilot-setup-steps.yml` | Instala el .NET 8 SDK para el cloud agent y el code review agéntico |
| `.github/pull_request_template.md` | Plantilla de Pull Request con la sección de decisiones sobre sugerencias de Copilot |
| `.vscode/extensions.json` | Extensiones recomendadas |
| `.vscode/mcp.json` | Ejemplo mínimo de servidor MCP para la demostración del Día 5 |

---

## Adaptación a otro dominio

El dominio (catálogo de productos) es deliberadamente neutral para evitar depender de reglas de negocio reales del cliente y eliminar el riesgo de exponer datos sensibles. Si prefiere acercarlo al dominio real del equipo:

1. **Conserve la estructura y las firmas.** Todo el material de laboratorio cita rutas, nombres de método y firmas exactas; cambiarlas obliga a revisar los diez laboratorios.
2. **Renombre solo la capa de dominio**: `Producto`, `Categoria` y los 12 registros de semilla en `InMemoryProductoRepository`. El resto puede permanecer intacto.
3. **Mantenga las reglas del descuento tal cual.** BUG-01, la historia de usuario HU-01 y todas las pruebas de casos borde dependen de esos umbrales, de la suma de descuentos y del tope del 20 %.
4. **No mueva los tres defectos ni los tres TODO** de archivo. Si lo hace, actualice `DEFECTOS_INTENCIONALES.md` y las referencias de línea de los laboratorios 5, 6 y 8.
5. Vuelva a ejecutar `verificar.sh` o `verificar.ps1` antes de entregar el repositorio a los participantes.

---

## Preparación antes del curso

1. Publique `inicial/` como repositorio en la organización de GitHub del cliente.
2. Conceda a cada participante permiso de **escritura**, o documente el flujo alternativo de fork.
3. Cree las ramas de referencia del instructor: `solucion/lab05`, `solucion/lab06`, `solucion/lab07`, `solucion/lab08`, y las de demostración `demo/demo05`, `demo/demo06`, `demo/demo08`.
4. Si hay rulesets o protección de rama activos, añada Copilot como **bypass actor** para que el cloud agent pueda operar.
5. Ejecute el script de verificación en una máquina limpia.

El procedimiento completo está en `04_guia_facilitador/checklist_previo.md`.
