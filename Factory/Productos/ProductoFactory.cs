using CafeGestion.Enums;
using CafeGestion.Models;

namespace CafeGestion.Factory.Productos;

public static class ProductoFactory
{
    public static IEnumerable<Cafe> SeedCafe()
    {
        var listaCafe = new List<Cafe>();
        listaCafe.Add(new Cafe {
            Id = 1, Nombre = "La Cima", Origen = TipoOrigen.Brasil,
            Region = "Minas Gerais", Variedad = TipoVariedad.Bourbon,
            Proceso = TipoProceso.Natural, Puntuacion = 8.3,
            NotaDeCata = "Chocolate con leche, frutal, toffee",
            Cantidad = 3, Disponible = true,
            Entrada = DateTime.Now, FechaTueste = new DateTime(2026, 4, 19)
        });

        listaCafe.Add(new Cafe {
            Id = 2, Nombre = "Yirgacheffe", Origen = TipoOrigen.Etiopia,
            Region = "Yirgacheffe", Variedad = TipoVariedad.Heirloom,
            Proceso = TipoProceso.Lavado, Puntuacion = 9.1,
            NotaDeCata = "Jazmín, bergamota, limón",
            Cantidad = 5, Disponible = true,
            Entrada = DateTime.Now, FechaTueste = new DateTime(2026, 3, 10)
        });

        listaCafe.Add(new Cafe {
            Id = 3, Nombre = "Huila", Origen = TipoOrigen.Colombia,
            Region = "Huila", Variedad = TipoVariedad.Castillo,
            Proceso = TipoProceso.Lavado, Puntuacion = 8.7,
            NotaDeCata = "Caramelo, manzana verde, panela",
            Cantidad = 8, Disponible = true,
            Entrada = DateTime.Now, FechaTueste = new DateTime(2026, 4, 1)
        });

        listaCafe.Add(new Cafe {
            Id = 4, Nombre = "Antigua", Origen = TipoOrigen.Guatemala,
            Region = "Antigua", Variedad = TipoVariedad.Bourbon,
            Proceso = TipoProceso.HoneyProcess, Puntuacion = 8.5,
            NotaDeCata = "Miel, nuez, cacao amargo",
            Cantidad = 2, Disponible = true,
            Entrada = DateTime.Now, FechaTueste = new DateTime(2026, 3, 25)
        });

        listaCafe.Add(new Cafe {
            Id = 5, Nombre = "Sumatra Mandheling", Origen = TipoOrigen.Indonesia,
            Region = "Sumatra", Variedad = TipoVariedad.Typica,
            Proceso = TipoProceso.Natural, Puntuacion = 8.1,
            NotaDeCata = "Tierra, cedro, chocolate negro",
            Cantidad = 4, Disponible = true,
            Entrada = DateTime.Now, FechaTueste = new DateTime(2026, 2, 15)
        });
        return listaCafe;
    }
}