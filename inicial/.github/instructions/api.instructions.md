---
description: "Convenciones para los controllers de la API de catálogo."
applyTo: "src/Catalogo.Api/Controllers/**/*.cs"
---

# Convenciones de API (controllers)

- Clases marcadas con `[ApiController]` y ruta explícita: `[Route("api/productos")]`.
- Un controller es **solo transporte HTTP**: sin reglas de negocio, sin consultas LINQ de dominio,
  sin acceso directo al repositorio. Todo se delega en `IProductoService`.
- Firma de las acciones: **`Task<ActionResult<T>>`** (o `Task<IActionResult>` cuando no hay cuerpo).
- Errores en formato **`ProblemDetails`** (`ValidationProblem`, `NotFound`, `Problem`).
- Cada acción declara sus contratos con **`[ProducesResponseType]`**:

```csharp
[HttpGet("{id:int}")]
[ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
public async Task<ActionResult<ProductoResponse>> ObtenerPorIdAsync(int id, CancellationToken cancellationToken)
```

- Códigos de estado: 200 lectura correcta, 201 creación (con `CreatedAtAction`), 204 eliminación,
  400 datos inválidos, 404 recurso inexistente.
- Restricciones de ruta tipadas: `{id:int}`.
- Documentación XML en cada acción para que Swagger la publique.
- El `CancellationToken` de la petición se recibe como parámetro y se propaga al servicio.
