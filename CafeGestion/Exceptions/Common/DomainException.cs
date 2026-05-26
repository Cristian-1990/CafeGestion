namespace CafeGestion.Exceptions.Common;
/// <summary>
/// Clase base de la que heredaran todas las excepciones, su única función es recibir y pasarselo a Exception
/// </summary>
/// <remarks>Todos heredan de Exception para poder implementar TRY-CATCH</remarks>>
/// <param name="message">Contiene el string que mostrará el tipo de error por pantalla</param>
public abstract class DomainException(string message) : Exception(message);