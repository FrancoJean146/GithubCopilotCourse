using Catalogo.Api.Models;

namespace Catalogo.Api.Dtos;

/// <summary>
/// Criterios de busqueda y paginacion del catalogo. Todos los filtros son opcionales.
/// </summary>
public sealed record BusquedaProductosQuery
{
    /// <summary>Tamano de pagina que se aplica cuando no se indica ninguno.</summary>
    public const int TamanoPaginaPredeterminado = 20;

    /// <summary>Tamano de pagina maximo admitido.</summary>
    public const int TamanoPaginaMaximo = 100;

    /// <summary>Categoria por la que se filtra. Si es <c>null</c> no se filtra por categoria.</summary>
    public Categoria? Categoria { get; init; }

    /// <summary>Precio minimo inclusivo. Si es <c>null</c> no se aplica cota inferior.</summary>
    public decimal? PrecioMinimo { get; init; }

    /// <summary>Precio maximo inclusivo. Si es <c>null</c> no se aplica cota superior.</summary>
    public decimal? PrecioMaximo { get; init; }

    /// <summary>Texto que debe contener el nombre del producto, sin distinguir mayusculas.</summary>
    public string? Texto { get; init; }

    /// <summary>Numero de pagina solicitado, empezando en 1.</summary>
    public int Pagina { get; init; } = 1;

    /// <summary>Cantidad de elementos por pagina. Se topa en <see cref="TamanoPaginaMaximo"/>.</summary>
    public int TamanoPagina { get; init; } = TamanoPaginaPredeterminado;
}
