using CafeGestion.Enums;

namespace CafeGestion.Models;
/// <summary>
/// Clase que hereda de Producto 
/// </summary>
public sealed record Cafe:Producto
{
    public TipoOrigen Origen { get; init; }
    public TipoVariedad Variedad { get; init; }
    public TipoProceso Proceso { get; init; }
    public string Region { get; init; } = string.Empty;
    public string NotaDeCata { get; init; } = string.Empty;
    public DateTime FechaTueste { get; init; }
/// <summary>
/// Devuelve toda la información de los atributos de un café
/// </summary>
/// <returns>String</returns>
    public override string ToString()
    {
        return $"Id: {Id}, Nombre: {Nombre}, Cantidad:{Cantidad}, Puntuacion: {Puntuacion}, Fecha de entrada: {Entrada}, Disponible{Disponible}, Origen: {Origen}, Variedad: {Variedad}, Proceso: {Proceso}, Region: {Region}, Notas de cata: {NotaDeCata}, Fecha de tueste: {FechaTueste}";
    }
};

/*
 */