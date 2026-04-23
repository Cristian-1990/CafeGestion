using CafeGestion.Models;
using CafeGestion.Service.Productos;
using CafeGestion.Storage.Common;
using CafeGestion.Storage.StorageJson;

namespace CafeGestion.Factory.Storages;

public static class RepoStorage
{
    public static IStorage<Producto> CreateStorage() => new StorageJson();
}