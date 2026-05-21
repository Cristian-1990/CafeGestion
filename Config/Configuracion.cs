namespace CafeGestion.Config;
/// <summary>
/// Clase estática que guarda la ruta y nombre de los ficheros.
/// Evita tener valores mágicos dispersos por el código.
/// </summary>
public static class Configuracion
{
    public static string DataFolder => "Data"; // Nombre de la carpeta dónde se guardaran los ficheros
    public static string CafesFile => Path.Combine(DataFolder, "cafes.json"); // Combina la carpeta "Data" con el nombre del archivo "cafes.json" para crear una ruta.
    public static string CafesFileXml => Path.Combine(DataFolder, "cafes.xml");
    public static string CafesFileCsv => Path.Combine(DataFolder, "cafes.csv");
    public static string TipoStorage => "json"; //Cambia a csv o xml sin necesidad de compilar
}