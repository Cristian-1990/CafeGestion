namespace CafeGestion.Storage;

/// <summary>
/// Interfaz para Guardar y Cargar ficheros genéricos
/// </summary>
/// <typeparam name="T">Tipo de dato(Json,CSV...)</typeparam>
public interface IStorage<T>
{
    /// <summary>
    /// Recibe una lista (items) y una ruta (path) donde guardarla.
    /// </summary>
    /// <param name="items">Lista a guardar</param>
    /// <param name="path">Ruta donde guardar esa lista</param>
    public void Guardar(IEnumerable<T> items, string path);
    /// <summary>
    /// Recibe una ruta y devuleve el objeto del tipo IEnumerable
    /// </summary>
    /// <param name="path">Ruta donde se encuentra el IEnumerable</param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<T> Cargar(string path);
}