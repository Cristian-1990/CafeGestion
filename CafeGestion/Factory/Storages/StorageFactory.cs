using CafeGestion.Models;
using CafeGestion.Config;
using CafeGestion.Storage.Common;
using CafeGestion.Storage.StorageJson;
using CafeGestion.Storage.StorageCsv;
using CafeGestion.Storage.StorageXml;

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
    public static IStorage<Producto> CreateStorage() => Configuracion.TipoStorage switch
    {
        "xml" => new StorageXml(),
        "csv" => new StorageCsv(),
        _     => new StorageJson() //Por defecto Json
    };
}