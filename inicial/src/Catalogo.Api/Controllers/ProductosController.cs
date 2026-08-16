using Catalogo.Api.Dtos;
using Catalogo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

/// <summary>
/// Endpoints HTTP del catalogo de productos.
/// </summary>
[ApiController]
[Route("api/productos")]
[Produces("application/json")]
public sealed class ProductosController : ControllerBase
{
    private readonly IProductoService _servicio;

    /// <summary>
    /// Crea el controller con sus dependencias.
    /// </summary>
    /// <param name="servicio">Servicio de catalogo de productos.</param>
    public ProductosController(IProductoService servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);

        _servicio = servicio;
    }

    /// <summary>
    /// Busca productos con filtros opcionales y paginacion.
    /// </summary>
    /// <param name="consulta">Criterios de busqueda y paginacion.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>Pagina de productos que cumplen los criterios.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<ProductoResponse>>> BuscarAsync(
        [FromQuery] BusquedaProductosQuery consulta,
        CancellationToken cancellationToken)
    {
        var productos = await _servicio.BuscarAsync(consulta, cancellationToken).ConfigureAwait(false);

        return Ok(productos);
    }

    /// <summary>
    /// Obtiene un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>El producto solicitado.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoResponse>> ObtenerPorIdAsync(int id, CancellationToken cancellationToken)
    {
        var producto = await _servicio.ObtenerPorIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (producto is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Producto no encontrado",
                Detail = $"No existe un producto con el identificador {id}.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(producto);
    }

    /// <summary>
    /// Crea un producto nuevo en el catalogo.
    /// </summary>
    /// <param name="solicitud">Datos del producto que se crea.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>El producto creado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoResponse>> CrearAsync(
        [FromBody] CrearProductoRequest solicitud,
        CancellationToken cancellationToken)
    {
        var creado = await _servicio.CrearAsync(solicitud, cancellationToken).ConfigureAwait(false);

        return CreatedAtAction(nameof(ObtenerPorIdAsync), new { id = creado.Id }, creado);
    }

    // TODO-01: falta el endpoint PATCH api/productos/{id}/precio que recibe ActualizarPrecioRequest,
    // calcula el precio final con IDescuentoCalculator, lo persiste y devuelve ProductoResponse
    // (200 correcto, 400 datos invalidos, 404 producto inexistente).

    /// <summary>
    /// Elimina un producto del catalogo.
    /// </summary>
    /// <param name="id">Identificador del producto que se elimina.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>Sin contenido si la eliminacion se realizo.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarAsync(int id, CancellationToken cancellationToken)
    {
        var eliminado = await _servicio.EliminarAsync(id, cancellationToken).ConfigureAwait(false);

        if (!eliminado)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Producto no encontrado",
                Detail = $"No existe un producto con el identificador {id}.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
}
