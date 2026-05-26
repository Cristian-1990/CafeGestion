using CafeGestion.Repository.Productos;
using CafeGestion.Repository.Productos.Base;

namespace CafeGestion.Factory.Repository;
using CafeGestion.Repository;
/// <summary>
/// Crea un nuevo repositorio
/// </summary>
public static class RepoFactory
{
    /// <summary>
    /// Devuelve un nuevo repositorio que implementa IProductoRepo
    /// </summary>
    /// <returns>Nuevo repositorio</returns>
    public static IProductoRepo CrearRepo() => new ProductoRepo();
}