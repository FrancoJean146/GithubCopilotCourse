# AGENTS.md

Instrucciones para cualquier agente de codificación (GitHub Copilot en VS Code y en GitHub,
Copilot CLI u otras herramientas compatibles con `AGENTS.md`) que trabaje en este repositorio.

## Qué es este repositorio

API REST de **catálogo de productos** en **.NET 8** (ASP.NET Core Web API con controllers).
Es el repositorio de práctica del curso corporativo de GitHub Copilot.

## Estructura

```text
CopilotLabCatalogo.sln
src/Catalogo.Api/
  Controllers/      Entrada HTTP. Solo transporte, sin reglas de negocio.
  Models/           Entidades de dominio (Producto, Categoria).
  Dtos/             Contratos de entrada y salida, inmutables (record).
  Repositories/     Acceso a datos. Implementacion en memoria con 12 productos de semilla.
  Services/         Reglas de negocio (busqueda, creacion, calculo de descuentos).
  Infrastructure/   Integraciones y middleware transversal.
tests/Catalogo.Api.Tests/   Pruebas unitarias con xUnit, FluentAssertions y Moq.
```

## Cómo compilar

```bash
dotnet restore CopilotLabCatalogo.sln
dotnet build CopilotLabCatalogo.sln --no-restore
```

## Cómo probar

```bash
dotnet test CopilotLabCatalogo.sln
```

## Cómo ejecutar

```bash
dotnet run --project src/Catalogo.Api/Catalogo.Api.csproj
```

Swagger UI queda disponible en `http://localhost:5080/swagger`.

## Reglas de trabajo

1. **No modifiques las pruebas existentes para hacerlas pasar.** Si una prueba falla, el defecto está
   en el código de producción; corrígelo ahí. Solo se pueden agregar pruebas nuevas.
2. Antes de terminar una tarea, ejecuta `dotnet build` y `dotnet test` y reporta la salida real.
3. Toda función pública nueva llega acompañada de su prueba unitaria y de su documentación XML en español.
4. Nunca agregues secretos al código ni a `appsettings.json`: usa variables de entorno o `dotnet user-secrets`.
5. No agregues paquetes NuGet nuevos sin declararlo en la respuesta y justificarlo.
6. Respeta las convenciones de `.editorconfig`, `.github/copilot-instructions.md` y los archivos de
   `.github/instructions/`.
7. Cambios pequeños y revisables: si una tarea toca más de cinco archivos, propón primero un plan.
