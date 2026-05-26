using CafeGestion.Dto;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Mapper;
using CafeGestion.Models;

namespace CafeGestion.Storage.StorageCsv;

public partial class StorageCsv : IStorageCsv
{
    private const string Cabecera = 
        "Id,Nombre,Cantidad,Puntuacion,Entrada,Disponible,Origen,Variedad,Proceso,Region,NotaDeCata,FechaTueste";

    public void Guardar(IEnumerable<Producto> items, string path)
    {
        try
        {
            var lineas = items.OfType<Cafe>().Select(c => c.ToDto()).Select(c =>
                $"{c.Id},{c.Nombre},{c.Cantidad},{c.Puntuacion},{c.Entrada:O},{c.Disponible},{c.Origen},{c.Variedad},{c.Proceso},{c.Region},{c.NotaDeCata},{c.FechaTueste:O}");

            File.WriteAllLines(path, lineas.Prepend(Cabecera));
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }

    public IEnumerable<Producto> Cargar(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("Archivo no encontrado");
        try
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Split(','))
                .Select(c => new CafeDto(
                    int.Parse(c[0]),
                    c[1],
                    int.Parse(c[2]),
                    double.Parse(c[3]),
                    DateTime.Parse(c[4]),
                    bool.Parse(c[5]),
                    c[6], c[7], c[8], c[9], c[10],
                    DateTime.Parse(c[11])
                ).ToModel());
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }
}