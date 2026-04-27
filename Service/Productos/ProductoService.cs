using CafeGestion.Config;
using CafeGestion.Enums;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Models;
using CafeGestion.Exceptions;
using CafeGestion.Repository.Productos.Base;
using CafeGestion.Storage.Common;
using CafeGestion.Validators;

namespace CafeGestion.Service.Productos;

/// <summary>
/// Intermediario entre lo que quiere hacer el program y las capas que realizan esas peticiones.
/// Coordina todas las operaciones
/// </summary>
/// <param name="repository"></param>
/// <param name="storage"></param>
/// <param name="validadorProducto"></param>
public class ProductoService(
    IProductoRepo repository,
    IStorage<Producto> storage,
    IValidador<Cafe> validadorProducto) : IProductoService
{
    public int TotalProductos => repository.GetAll().Count(); // total de productos creados

    /// <summary>
    /// Devuelve la lista de todos los Productos existentes en el repositorio
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Producto> GetAll()
    {
        return repository.GetAll();
    }

    /// <summary>
    /// Devuelve ina lista ordenada segun el criterio indicado
    /// </summary>
    /// <param name="orden">Criterio de ordenación</param>
    /// <param name="filtro">Condicion para filtrar resultados</param>
    /// <returns>IEnumerable ordenado</returns>
    public IEnumerable<Producto> GetAllOrderBy(TipoOrdenamiento orden = TipoOrdenamiento.Id,
        Predicate<Producto>? filtro = null)
    {
        return GetAllOrderBy(TipoOrdenamiento.Id, p => p is Producto).Cast<Producto>();
    }

    /// <summary>
    /// Devuelve un producto en función del Id introducido
    /// </summary>
    /// <param name="id">Clave del producto a buscar</param>
    /// <returns>Producto, null si no existe</returns>
    public Producto? GetById(int id)
    {
        var producto = repository.GetById(id);
        return producto;
    }

    /// <summary>
    /// Crea un nuevo Producto y lo almacena
    /// </summary>
    /// <param name="producto">Nuevo producto a crear</param>
    /// <returns>Producto nuevo creado</returns>
    public Producto Guardar(Producto producto)
    {
        var nuevoProducto = repository.Create(producto);
        return nuevoProducto;
    }

    /// <summary>
    /// Busca un Producto y actualiza sus datos (si no existe devuelve una excepción)
    /// </summary>
    /// <param name="id">Identificador del producto</param>
    /// <param name="producto">Producto con los valores actualizados</param>
    /// <returns>Producto actaulizado</returns>
    /// <exception cref="ProductosException"></exception>>
    public Producto Actualizar(int id, Producto producto)
    {
        var actualizada = repository.Update(id, producto) ?? throw new ProductosException.NotFound(id.ToString());
        return actualizada;
    }

    /// <summary>
    /// Busca por id y borra el producto
    /// </summary>
    /// <param name="id">Identificador del producto</param>
    /// <returns>Producto que ha sido borrado</returns>
    public Producto Delete(int id)
    {
        var productoBorrado = repository.Delete(id);
        return productoBorrado;
    }

    public bool Existe(int id)
    {
        var existe = repository.Existe(id);
        return existe;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public InformeProducto GenerarInforme()
    {
        return new InformeProducto();
    }

    /// <summary>
    /// Carga en memoria elementos que se han importado de un archivo concreto(DataFolder)
    /// </summary>
    /// <remarks>Crea una lista cargando una ruta predefinida en la configuracion, para cada elemento en la lista llama al método "Create" del repository</remarks>>
    /// <returns>Total de elementos importados</returns>
    /// <exception cref="StorageError">Lanza excepción si no consigue crear la lista</exception>
    public int ImportarDatos()
    {
        try
        {
            var productos = storage.Cargar(Configuracion.CafesFile);
            repository.DeleteAll();

            var contador = 0;
            foreach (var p in productos)
            {
                Guardar(p);
                contador++;
            }

            return contador;
        }
        catch (Exception ex)
        {
            throw new ProductosException.StorageError(ex.Message);
        }
    }

    /// <summary>
    /// Guarda de forma persistente los datos en memoria
    /// </summary>
    /// <remarks>Crea una lista a partir del repositorio existente actual, guarda esa lista en la configuración predeterminada</remarks>>
    /// <returns>Total de elementos guardados</returns>
    /// <exception cref="StorageError">Se lanza si no consigue completar el guardado</exception>
    public int ExportarDatos()
    {
        try
        {
            var productos = repository.GetAll();
            storage.Guardar(productos, Configuracion.CafesFile);
            var totalExportados = productos.Count();
            return totalExportados;
        }
        catch (Exception ex)
        {
            throw new ProductosException.StorageError(ex.Message);
        }
    }
}

/*
    public bool Exist(int id)
    {
        var existe = repository.
    }
*/
