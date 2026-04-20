using CafeGestion.Models;
using CafeGestion.Repository.Productos.Base;
using CafeGestion.Repository.Common;

namespace CafeGestion.Repository.Productos;
/// <summary>
/// Clase para Productos del repositorio con las funciones CRUD implementadas.
/// </summary>
public class ProductoRepo : IProductoRepo
{
    private readonly Dictionary<int, Producto> _diccionarioCafe = new();//Crea un nuevo diccionario donde la clave será el Id.
    private int _contadorId; //Contador de instancias
    
    /// <summary>
    /// Muestra un listado de todos los productos
    /// </summary>
    /// <returns>Diccionario de productos</returns>
    /// <inheritdoc cref="ICrudRepository{TKey,TEntity}"/>
    public IEnumerable<Producto> GetAll()
    {
        return _diccionarioCafe.Values;
    }

    /// <summary>
    /// Muestra el producto encontrado en funcion del id, o null si no existe
    /// </summary>
    /// <param name="id">Clave para buscar el producto</param>
    /// <returns>Producto o null</returns>
    /// <inheritdoc cref="ICrudRepository{TKey,TEntity}"/>>
    public Producto? GetById(int id)
    {
        return _diccionarioCafe.GetValueOrDefault(id);
    }
    /// <summary>
    /// Crea un nuevo producto si no existe ya
    /// </summary>
    /// <param name="entity">Producto a crear</param>
    /// <returns>Producto o null</returns>
    /// <inheritdoc cref="ICrudRepository{TKey,TEntity}"/>>
    public Producto? Create(Producto entity)
    {
        if (Existe(entity.Id)) return null;
        var nuevoCafe = entity with
        {
            Id = _contadorId++,
        };
        _diccionarioCafe[nuevoCafe.Id] = nuevoCafe;
        return nuevoCafe;
    }

    public Producto? Update(int id, Producto entity)
    {
        if (!_diccionarioCafe.TryGetValue(id, out var actual)) return null;

        var productoActualizado = entity with
        {
            Disponible = true
        };
        _diccionarioCafe[id] = productoActualizado;
        return productoActualizado;
    }
    /// <summary>
    /// Busca un producto y lo borra
    /// </summary>
    /// <param name="id">Clave unica del producto a borrar</param>
    /// <returns>Producto?</returns>
    /// <inheritdoc cref="ICrudRepository{TKey,TEntity}"/>>
    public Producto? Delete(int id)
    {
        if (!_diccionarioCafe.Remove(id, out var cafe)) return null;
        return cafe with
        {
            Disponible = false
        };
    }
    /// <summary>
    /// Comprueba si un Producto existe buscando su id
    /// </summary>
    /// <param name="id">id del Producto comprobar</param>
    /// <returns>bool</returns>
    public bool Existe(int id)
        {
            return _diccionarioCafe.ContainsKey(id);
        }
    /// <summary>
    /// Comprueba si el producto está disponible
    /// </summary>
    /// <param name="id">id del Producto comprobar</param>
    /// <returns>bool</returns>
    public bool Disponible(int id)
        {
            return _diccionarioCafe.ContainsKey(id);
        }
};
