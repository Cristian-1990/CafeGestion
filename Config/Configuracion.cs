namespace CafeGestion.Config;
/// <summary>
/// Clase estatica que guarda la ruta y nombre de los ficheros.
/// Evita tener valores mágicos dispersos por el código.
/// </summary>
public static class Configuracion
{
    public static string DataFolder => "Data"; // Nombre de la carpeta dónde se guardaran los ficheros
    public static string CafesFile => Path.Combine(DataFolder, "cafes.json"); // Combina la carpeta "Data" con el nombre del archivo "cafes.json" para crear una ruta.
}