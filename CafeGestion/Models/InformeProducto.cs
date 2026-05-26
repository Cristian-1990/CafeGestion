namespace CafeGestion.Models;

/// <summary>
/// Devuelve un informe con toda la información de un producto
/// </summary>
public sealed record InformeProducto
{
    public IEnumerable<Producto> PorPuntuacion { get; init; } = Enumerable.Empty<Producto>();
    public int TotalProductos { get; init; } 
    
}