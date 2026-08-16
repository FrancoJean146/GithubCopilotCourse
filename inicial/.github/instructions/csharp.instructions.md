---
description: "Convenciones de C# para el catálogo de productos (.NET 8)."
applyTo: "**/*.cs"
---

# Convenciones de C#

## Estructura de archivo

- `namespace` con ámbito de archivo: `namespace Catalogo.Api.Services;`
- `using` fuera del namespace, `System.*` primero, sin `using` sin utilizar.
- Un tipo público por archivo.

## Tipos y miembros

- `sealed` por defecto en las clases que no se diseñan para herencia.
- Campos privados `readonly` y en `_camelCase`; se asignan solo en el constructor.
- Dependencias por constructor, validadas con `ArgumentNullException.ThrowIfNull(...)`.
- DTO como `record` inmutable; entidades de dominio como `class` con propiedades.
- `IReadOnlyList<T>` o `IReadOnlyCollection<T>` como tipo de retorno público para colecciones.

## Asincronía

- Sufijo `Async` obligatorio y `CancellationToken cancellationToken = default` como último parámetro.
- Nunca `async void`; nunca `.Result` ni `.Wait()`.
- Usa `Task.FromResult` cuando la implementación es sincrónica (repositorio en memoria).

## Errores y validación

- Argumentos fuera de rango: `ArgumentOutOfRangeException` con `nameof(parametro)`.
- Datos de entrada inválidos: `ValidationException` (DataAnnotations) desde la capa de servicio.
- Recurso inexistente: se devuelve `null` desde el servicio y el controller responde 404.
- Prohibido capturar `Exception` para ignorarla.

## Dinero y cálculos

- `decimal` para importes, nunca `double`.
- Redondeo explícito: `Math.Round(valor, 2, MidpointRounding.AwayFromZero)`.

## Documentación

- Todo miembro público lleva documentación XML (`///`) en español, con `<param>`,
  `<returns>` y `<exception>` cuando corresponda.
