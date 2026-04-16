namespace CafeGestion.Models;

public abstract record Producto
{
    public int Id { get; init; } 
    public string Nombre { get; init; }
    public int Cantidad { get; init; }
    public double Putnuacion { get; init; }
    public DateTime Entrada { get; init; } = DateTime.Now;
    public bool Disponible { get; init; } = true;
/// <summary>
/// Compara el HasCode de dos objetos para saber si son identicos (misma posición en memoria)
/// </summary>
/// <returns>Devuelve bool, afirmativo si son el mismo objeto</returns>
    public virtual int GetHasCode()
    {
        return HashCode.Combine(Id);
    }
/// <summary>
/// Compara el valor del atributo Id de dos objetos para saber si tienen el mismo valor
/// </summary>
/// <param name="other"></param>
/// <returns>Devuelve verdadero si tienen el mismo valor, o false si no lo tienen</returns>
    public virtual bool Equals(Producto? other)
    {
        return other is not null && Id == other.Id;
    }
};