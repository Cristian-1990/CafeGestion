using CafeGestion.Models;
using CafeGestion.Storage.Common;

namespace CafeGestion.Storage.StorageXml;
/// <summary>
/// Interfaz que define el tipo de IStorage en formato XML.
/// </summary>
public interface IStorageXml : IStorage<Producto> { }