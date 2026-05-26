namespace CafeGestion.Service.Productos;
using CafeGestion.Models;
using CafeGestion.Enums;
/// <summary>Intertfaz genérica del servicio </summary>
/// <remarks>Coordina el Repository, el Storage y el Validador</remarks>>
public interface IProductoService
{
    /// <summary>Lleva la cuenta de los productos que se han instanciado</summary>
    int TotalProductos { get; }
    /// <summary>Devuelve una lista con todos los objetos del tipo producto.</summary>
    /// <returns>IEnumerable</returns>
    IEnumerable<Producto> GetAll();
    /// <summary>Devuelve una lista ordenada de todos los productos</summary>
    /// <param name="orden">Criterio de ordenación de la lista</param>
    /// <param name="filtro">Condicion de la busqueda</param>
    /// <returns>IEnumerable</returns>
    IEnumerable<Producto> GetAllOrderBy(TipoOrdenamiento orden = TipoOrdenamiento.Id,Predicate<Producto>? filtro = null);
    /// <summary>Devuelve un Producto si existe en función del id que se le pasa como parámetro</summary>
    /// <param name="id">Id a buscar</param>
    /// <returns>Producto o null</returns>
    Producto? GetById(int id);
   /// <summary>Guarda los datos del producto que le pasemos como parámetro</summary>
   /// <param name="producto">Producto que queremos guardar</param>
   /// <returns></returns>
    Producto Guardar(Producto producto);
   /// <summary>Busca un producto y si existe lo actualiza</summary>
   /// <param name="producto">Producto a buscar para actualizar</param>
   /// <returns>Producto</returns>
    Producto Actualizar(int id, Producto producto);
   /// <summary>Busca y borra los datos de un producto existente</summary>
   /// <param name="id">Id por el que buscar el producto</param>
   /// <returns>Producto</returns>
    Producto Delete(int id);

 /// <summary>
 /// Comprueba si un producto existe buscando por Id.
 /// </summary>
 /// <param name="id">Clave del producto a buscar</param>
 /// <returns>bool</returns>
    public bool Existe(int id);
    /// <summary>Muestra la lista de productos existentes de un almacenamiento persistente</summary>
    /// <returns>Total de productos importados</returns>
    // InformeProducto GenerarInforme();

    int ImportarDatos();
   /// <summary>Exporta el listado de productos a un almacenamiento persistente</summary>
   /// <returns>Total de productos exportados</returns>
   int ExportarDatos();

}