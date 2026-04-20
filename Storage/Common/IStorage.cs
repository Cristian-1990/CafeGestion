namespace CafeGestion.Storage.Common;

public interface IStorage<T>
{
    /// <summary>
    ///Recibe y Guarda un IEnumerable en la ruta que define el string.
    /// </summary>
    /// <param name="item">Ienumerable a Guardar</param>
    /// <param name="path">Ruta donde se guarda el IEnumerable</param>
    public void Guardar(IEnumerable<T> item, string path);
    /// <summary>
    /// Recibe una ruta y devuelve(Carga) el IEnumerable que contiene
    /// </summary>
    /// <param name="path">Nombre de la ruta de destino</param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<T> Cargar(string path);
}