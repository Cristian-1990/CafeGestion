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
   /// <summary>
   /// 
   /// </summary>
   /// <remarks>Café genera automaticamente su propio equals que compara todos los campos al ser sealed record</remarks>
   /// <param name="other"></param>
   /// <returns></returns>
   /// <summary>
   /// Compara dos cafés por su Id ignorando el resto de campos.
   /// </summary>
   /// <remarks>Café genera automáticamente su propio Equals que compara todos los campos al ser sealed record, por eso es necesario sobreescribir equals</remarks>
   /// <param name="other">Café con el que se compara</param>
   /// <returns>True si tienen el mismo Id, false si no</returns>
   public bool Equals(Cafe? other) => other is not null && Id == other.Id;

   /// <summary>
   /// Calcula el código hash basado exclusivamente en el Id para mantener coherencia con Equals.
   /// </summary>
   /// <returns>Código hash entero basado en el Id</returns>
   public override int GetHashCode() => HashCode.Combine(Id);

};

/*
 */