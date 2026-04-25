using CafeGestion.Models;
using CafeGestion.Storage.Common;

namespace CafeGestion.Storage.StorageJson;
/// <summary>
/// Interfaz  que define el tipo de IStorage que se hereda en formato JSON.
/// </summary>
public interface IStorageJson : IStorage<Producto>{};