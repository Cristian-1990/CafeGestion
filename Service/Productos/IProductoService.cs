namespace CafeGestion.Service.Productos;
using CafeGestion.Models;
using CafeGestion.Enums;

public interface IProductoService
{
    int TotalProductos { get; }
    IEnumerable<Producto> GetAll();
    IEnumerable<Producto> GetAllOrderBy(TipoOrdenamiento orden = TipoOrdenamiento.Id,Predicate<Producto>? filtro = null);
    Producto? GetById(int id);
    Producto Guardar(Producto producto);
    Producto Actualizar(Producto producto);
    Producto Delete(int id);
   // InformeProducto GenerarInforme();
    int ImportarDatos();
    int ExportarDatos();

}