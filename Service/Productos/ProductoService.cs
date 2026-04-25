using CafeGestion.Config;
using CafeGestion.Enums;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Models;
using CafeGestion.Repository.Productos.Base;
using CafeGestion.Storage.Common;
using CafeGestion.Validators;

namespace CafeGestion.Service.Productos;

public class ProductoService(
    IProductoRepo repository,
    IStorage<Producto> storage,
    IValidador<Cafe> validadorProducto):IProductoService
{
    public int TotalProductos => repository.GetAll().Count();
    public IEnumerable<Producto> GetAll()
    {
        return repository.GetAll();
    }
    public IEnumerable<Producto> GetAllOrderBy(TipoOrdenamiento orden = TipoOrdenamiento.Id, Predicate<Producto>? filtro = null)
    {
        return GetAllOrderBy(TipoOrdenamiento.Id, p => p is Producto).Cast<Producto>();
    }

    public Producto? GetById(int id)
    {
        var producto = repository.GetById(id);
        return producto;
    }

    public Producto Guardar(Producto producto)
    {
        var nuevoProducto = repository.Create(producto);
        return nuevoProducto;
    }

    public Producto Actualizar(Producto producto)
    {
        var actualizada = repository.Create(producto) ?? throw new ProductosException.AlreadyExist(producto.Id);
        return actualizada;
    }

    public Producto Delete(int id)
    {
        var productoBorrado = repository.Delete(id);
        return productoBorrado;
    }

    public InformeProducto GenerarInforme()
    {
        return new InformeProducto();
    }
    

    public int ImportarDatos()
    {
        try {
            
            var productos = storage.Cargar(Configuracion.DataFolder);
            repository.DeleteAll();

            var contador = 0;
            foreach (var p in productos) {
                Guardar(p);
                contador++;
            }
            return contador;
        }
        catch (Exception ex) {
            throw new ProductosException.StorageError(ex.Message);
        }
    }
    public int ExportarDatos()
    {
        try
        {
            var productos = repository.GetAll();
            var count = productos.Count();
            return count;
        }
        catch (Exception ex)
        {
            throw new ProductosException.StorageError(ex.Message);
        }
    }
}