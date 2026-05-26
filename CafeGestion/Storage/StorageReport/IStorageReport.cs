using CafeGestion.Models;

namespace CafeGestion.Storage.StorageReport;

/// <summary>
/// Interfaz que define la generación de fichas de café en HTML y PDF.
/// </summary>
public interface IStorageReport
{
    /// <summary>Genera una ficha HTML con los datos del café.</summary>
    /// <param name="cafe">Café del que se genera la ficha.</param>
    /// <param name="path">Ruta donde se guarda el fichero.</param>
    void GuardarHtml(Cafe cafe, string path);

    /// <summary>Genera una ficha PDF con los datos del café.</summary>
    /// <param name="cafe">Café del que se genera la ficha.</param>
    /// <param name="path">Ruta donde se guarda el fichero.</param>
    void GuardarPdf(Cafe cafe, string path);
}