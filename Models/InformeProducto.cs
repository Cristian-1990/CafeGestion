namespace CafeGestion.Models;

public sealed record InformeProducto
{
    public IEnumerable<Producto> PorPuntuacion { get; init; } = Enumerable.Empty<Producto>();
    public int TotalProductos { get; init; } 
    
}