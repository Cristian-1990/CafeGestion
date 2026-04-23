using CafeGestion.Repository.Common;
using CafeGestion.Repository.Productos;
using CafeGestion.Repository.Productos.Base;

namespace CafeGestion.Factory.Repository;
using CafeGestion.Repository;
public static class RepoFactory
{
    public static IProductoRepo CrearRepo() => new ProductoRepo();
}