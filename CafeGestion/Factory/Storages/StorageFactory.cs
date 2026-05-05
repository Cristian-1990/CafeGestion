using CafeGestion.Models;
using CafeGestion.Service.Productos;
using CafeGestion.Storage.Common;
using CafeGestion.Storage.StorageJson;

namespace CafeGestion.Factory.Storages;
/// <summary>
/// Crea un nuevo Storage
/// </summary>
public static class StorageFactory
{
    /// <summary>
    /// Crea un nuevo Storage del tipo producto que implementa IStorage
    /// </summary>
    /// <returns>Nuevo storage</returns>
    public static IStorage<Producto> CreateStorage() => new StorageJson();
}