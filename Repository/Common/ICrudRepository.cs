using CafeGestion.Models;

namespace CafeGestion.Repository.Common;
/// <summary>
/// Implementa un CRUD que recibirá dos parámetros genéricos, con restricción en TEntity, debe ser del tipo class y NO primitivo
/// </summary>
/// <typeparam name="TKey">Primer parámetro del tipo T</typeparam>
/// <typeparam name="TEntity">Segundo parámetro del tipo T con restricción de class</typeparam>
public interface ICrudRepository <TKey, TEntity> where TEntity : class
{
    /// <summary>
    /// Devuelve un listado de todas las entidades Café
    /// </summary>
    /// <returns>Listado IEnumerable </returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Función que busca por Id y devuelve un TEntity
    /// </summary>
    /// <param name="id">Parámetro por el que se hará la búsqueda</param>
    /// <returns>TEntity encontrado</returns>
    TEntity GetById(TKey id);

    /// <summary>
    /// Crea un nuevo objeto genérico TEntity
    /// </summary>
    /// <param name="entity">Parámetro de entrada Genérico</param>
    /// <returns>Nuevo TEntity </returns>
    TEntity? Create(TEntity entity);
    /// <summary>
    /// Busca por Id una entidad T y la actualiza
    /// </summary>
    /// <param name="id">Parámetro de búsqueda</param>
    /// <param name="entity">Tipo que debe encontrar la búsqueda por Id</param>
    /// <returns>TEntity actualizada</returns>
    TEntity Update(TKey id, TEntity entity);
    /// <summary>
    /// Busca por id un tipo genérico y lo borra si existe, y si no, devuelve null
    /// </summary>
    /// <param name="id">parámetro de búsqueda</param>
    /// <returns>Entidad borrada o null</returns>
    TEntity? Delete(TKey id);
    

}