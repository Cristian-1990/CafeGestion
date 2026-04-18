using CafeGestion.Models;

namespace CafeGestion.Repository.Productos;
/// <summary>
/// Interfaz que implementa la ICrudRepository y añade función para los de tipo Producto
/// </summary>
public interface IProductoRepo : ICrudRepository<int,Producto >
{
    /// <summary>
    ///Muestra la disponibilidad de un Café 
    /// </summary>
    /// <param name="disponible">True si hay existencias </param>
    /// <returns></returns>
    bool Disponible(bool disponible);
}