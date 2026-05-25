namespace CafeGestion.Models;
/// <summary>
/// Clase base producto 
/// </summary>
public abstract record Producto
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public int Cantidad { get; init; }
    public double Puntuacion { get; init; }
    public DateTime Entrada { get; init; } = DateTime.Now;
    public bool Disponible { get; init; } = true;
/// <summary>
/// Redefine la función HasCode y Crea un número entero que representa el objeto 
/// </summary>
/// <returns>Devuelve un int que representa al objeto</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
/// <summary>
/// Compara el valor del atributo Id de dos objetos para saber si tienen el mismo valor
/// </summary>
/// <remarks> Crea una funcion nueva en lugar de sobreescribir la existente con "Producto?", en lugar de "Object?. Para ayudar al compilador"</remarks>>
/// <param name="other">Objeto con el que se compara</param>
/// <returns>Devuelve verdadero si tienen el mismo valor, o false si no lo tienen</returns>
    public virtual bool Equals(Producto? other)
    {
        return other is not null && Id == other.Id;
    }
};