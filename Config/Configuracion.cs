namespace CafeGestion.Config;

public static class Configuracion
{
    public static string DataFolder => "Data";
    public static string CafesFile => Path.Combine(DataFolder, CafesFile);
}