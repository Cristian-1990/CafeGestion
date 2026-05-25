using System.Xml.Linq;
using CafeGestion.Dto;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Mapper;
using CafeGestion.Models;

namespace CafeGestion.Storage.StorageXml;

public class StorageXml : IStorageXml
{
    public void Guardar(IEnumerable<Producto> items, string path)
    {
        try
        {
            var xml = new XDocument(
                new XElement("Cafes",
                    items.OfType<Cafe>().Select(c => c.ToDto()).Select(c =>
                        new XElement("Cafe",
                            new XElement("Id", c.Id),
                            new XElement("Nombre", c.Nombre),
                            new XElement("Cantidad", c.Cantidad),
                            new XElement("Puntuacion", c.Puntuacion),
                            new XElement("Entrada", c.Entrada),
                            new XElement("Disponible", c.Disponible),
                            new XElement("Origen", c.Origen),
                            new XElement("Variedad", c.Variedad),
                            new XElement("Proceso", c.Proceso),
                            new XElement("Region", c.Region),
                            new XElement("NotaDeCata", c.NotaDeCata),
                            new XElement("FechaTueste", c.FechaTueste)))));
            xml.Save(path);
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }

    public IEnumerable<Producto> Cargar(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("Archivo no encontrado");
        try
        {
            return XDocument.Load(path).Root!.Elements("Cafe").Select(e =>
                new CafeDto(
                    int.Parse(e.Element("Id")!.Value),
                    e.Element("Nombre")!.Value,
                    int.Parse(e.Element("Cantidad")!.Value),
                    double.Parse(e.Element("Puntuacion")!.Value),
                    DateTime.Parse(e.Element("Entrada")!.Value),
                    bool.Parse(e.Element("Disponible")!.Value),
                    e.Element("Origen")!.Value,
                    e.Element("Variedad")!.Value,
                    e.Element("Proceso")!.Value,
                    e.Element("Region")!.Value,
                    e.Element("NotaDeCata")!.Value,
                    DateTime.Parse(e.Element("FechaTueste")!.Value)
                ).ToModel());
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }
}