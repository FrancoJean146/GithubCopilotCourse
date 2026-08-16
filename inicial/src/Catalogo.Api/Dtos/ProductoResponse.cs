using Catalogo.Api.Models;

namespace Catalogo.Api.Dtos;

/// <summary>
/// Representacion de un producto tal como la devuelve la API.
/// </summary>
/// <param name="Id">Identificador unico del producto.</param>
/// <param name="Nombre">Nombre comercial del producto.</param>
/// <param name="Categoria">Categoria a la que pertenece el producto.</param>
/// <param name="PrecioBase">Precio unitario vigente del producto.</param>
/// <param name="Existencias">Unidades disponibles en inventario.</param>
/// <param name="Activo">Indica si el producto se publica en el catalogo.</param>
public sealed record ProductoResponse(
    int Id,
    string Nombre,
    Categoria Categoria,
    decimal PrecioBase,
    int Existencias,
    bool Activo);
