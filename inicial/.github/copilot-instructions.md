# Instrucciones de repositorio para GitHub Copilot

Estas instrucciones se aplican a **todo** el repositorio y Copilot las incluye automáticamente
como contexto en Copilot Chat, en la edición con Copilot y en el modo agente.

## Contexto del proyecto

- API REST de **catálogo de productos** construida con **.NET 8** (ASP.NET Core Web API, controllers).
- Capas: `Controllers` (HTTP) -> `Services` (reglas de negocio) -> `Repositories` (persistencia en memoria).
- El proyecto es material de formación: la claridad del código es más importante que la micro-optimización.

## Idioma

- **Comentarios, documentación XML, mensajes de commit y descripciones de PR: en español.**
- **Identificadores (clases, métodos, propiedades, variables): en inglés técnico o en español sin acentos ni eñes**, nunca mezclando ambos en el mismo nombre.
- No uses acentos ni caracteres especiales en identificadores.

## Reglas obligatorias

1. **.NET 8** como único framework de destino (`net8.0`). No propongas APIs de versiones posteriores.
2. Todo método asincrónico devuelve `Task`/`Task<T>`, termina con el sufijo **`Async`** y acepta un
   `CancellationToken` opcional que se propaga hacia abajo.
3. **Inyección de dependencias por constructor**. Nada de `new` para servicios, ni service locator,
   ni singletons estáticos mutables.
4. **Prohibido `Console.WriteLine`** en código de producción: usa `ILogger<T>`.
5. **Nunca escribas secretos en el código ni en `appsettings.json`.** Usa variables de entorno,
   `dotnet user-secrets` o un gestor de secretos.
6. **Toda entrada pública se valida** (DataAnnotations en los DTO y validación explícita en el servicio).
   Los datos inválidos producen un error 400 con `ProblemDetails`.
7. Los **DTO son inmutables**: se declaran como `record` con propiedades `init` o posicionales.
8. Las **excepciones de dominio no se capturan silenciosamente**: se propagan al middleware de errores,
   que las traduce a `ProblemDetails`. Prohibido `catch { }`.
9. **Toda función pública nueva requiere su prueba unitaria** en `tests/Catalogo.Api.Tests`.
10. Habilitado `nullable`: no uses `!` para silenciar el compilador sin justificarlo en un comentario.

## Estilo

- Cuatro espacios de indentación, `namespace` con ámbito de archivo (`file-scoped`).
- `var` cuando el tipo es evidente en la misma línea; tipo explícito cuando no lo es.
- Un tipo público por archivo, con el nombre del archivo igual al del tipo.
- Miembros públicos en `PascalCase`; campos privados en `_camelCase`.

## Qué NO hacer

- No agregues paquetes NuGet nuevos sin indicarlo explícitamente en la respuesta.
- No modifiques las pruebas existentes para que pasen: corrige el código de producción.
- No introduzcas dependencias de red o de base de datos reales: el repositorio es en memoria.
