using CafeGestion.Exceptions.Common;

namespace CafeGestion.Exceptions.Productos;
///<summary>Contiene todas las excepciones de Producto</summary>
public abstract class ProductosException(string message) : DomainException(message)
{
    ///<summary>Se lanza cuando se busca por Id y no encuenta ningun resultado en el repositorio</summary>
    public sealed class NotFound(string id) : ProductosException($"No se encontró ningun producto con el ID: {id}");
///<summary>Se lanza cuadno existen errores en la validacion de datos</summary>
    public sealed class Validation(IEnumerable<string> errors) : ProductosException("Se han detectado errores")
    {
        public IEnumerable<string> Errores { get; init; } = errors;
    };
///<summary>Se  lanza al intentar un crear un  producto con un id que ya existe en el repositorio</summary>
    public sealed class AlreadyExist(int id) : ProductosException($"El producto con ID : {id} ya existe.");
///<summary>Se lanza si existe algun error de lectura o escritura </summary>
    public sealed class StorageError(string details) : ProductosException($"Error de almacenamiento {details}");
}