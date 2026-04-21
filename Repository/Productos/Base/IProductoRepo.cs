using CafeGestion.Models;
using CafeGestion.Repository.Common;


namespace CafeGestion.Repository.Productos.Base;
/// <summary>
/// Interfaz que implementa la ICrudRepository y añade función para los de tipo Producto
/// </summary>
public interface IProductoRepo : ICrudRepository<int,Producto >
{
    ///  <summary>
    /// Muestra la disponibilidad de un Café 
    ///  </summary>
    ///  <returns></returns>
    bool Disponible(int id);

    public bool DeleteAll();

}