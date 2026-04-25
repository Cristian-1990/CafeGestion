using System.Text.Json;
using CafeGestion.Dto;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Mapper;
using CafeGestion.Models;

namespace CafeGestion.Storage.StorageJson;
/// <summary>
/// Clase que implementa dos metodos para Guardar y Cargar ficheros (Escribir y leer)
/// </summary>
public class StorageJson : IStorageJson
{/// <summary>
 /// Metodo para guardar una Cafe en una ruta
 /// </summary>
 /// <param name="items">Ienumerable del tipo Cafe</param>
 /// <param name="path">Ruta donde se guarda items del tipo string</param>
    public void Guardar(IEnumerable<Producto> items, string path)
    {
        try
        {
            using var stream = File.Create(path); //Crea el fichero en la ruta indicada, si existe lo sobreescribe
            var dto = items
                .OfType<Cafe>()
                .Select(c => c.ToDto()) //Por cada Cafe llama a CafeDto 
                .ToList();//Materializa el resultado de select en forma de lista
            JsonSerializer.Serialize(stream, dto); //Convierte la lista CafeDto a JSon
        }
        catch (Exception ex)
        {
           throw new ProductosException.StorageError(ex.Message);
        }
    }
/// <summary>
/// Carga (lee) los archivos que contiene el path
/// </summary>
/// <param name="path"></param>
/// <returns>IEnumerable</returns>
/// <exception cref="FileNotFoundException">No se encuentra el archivo</exception>
/// <exception cref="ProductosException">No se pueden cargar </exception>
    public IEnumerable<Producto> Cargar(string path)
    {
        if (!Path.Exists(path))//Comprueba si el fichero existe antes de intentar leerlo
        {
            throw new FileNotFoundException("Archivo no encontrado"); // Si no existe lanza excepcion
        }
        try
        {
            using var stream = File.OpenRead(path); // Abre el fichero de la ruta  indicada en "path" (solo modo lectura)
            var dto = JsonSerializer.Deserialize<List<CafeDto>>(stream); // Deserealiza el fichero Json en el tipo CafeDto que se encuentran en stream
            return dto?//Devuelve dto que puede ser nulo. Si es null no ejecuta el Select
                .Select(dto => dto.ToModel())//Por cada CafeDto convierte  Cafe
                   ?? throw new ProductosException.StorageError("Imposible desearilizar"); //Si existe un CafeDto nulo, lanza excepción
        }
        catch (Exception ex)
        {
           throw new ProductosException.StorageError("Error, no se pueden cargar los archivos"); 
           
        }
    }
}