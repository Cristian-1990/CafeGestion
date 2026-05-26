namespace CafeGestion.Validators;
/// <summary>
/// Interfaz genérica del validador
/// </summary>
/// <typeparam name="T">Tipo del objeto que queremos validar</typeparam>
public interface IValidador <T> {
    /// <summary>
    /// Recibe un objeto genérico para devolver una lista string de errores
    /// </summary>
    /// <param name="producto">Objeto genérico a validar</param>
    /// <returns>IEnumerable drl tipo string</returns>
    IEnumerable<string> Validar(T producto);
}