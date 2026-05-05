using CafeGestion.Models;
using CafeGestion.Enums;
namespace CafeGestion.Validators;
/// <summary>
///Clase que implementa y define el uso de la interfaz IValidador en entidades del tipo Café
/// </summary>
public class ValidadorCafe: IValidador<Cafe>
{
    /// <summary>
    /// Recibe un cafe para validar, almacena los posibles errores en una lista string
    /// </summary>
    /// <param name="cafe">Entidad del tipo cafe que validaremos</param>
    /// <returns>IEnumerable del tipo String(lista de errores)</returns>
    public IEnumerable<string> Validar(Cafe cafe)
    {
        //Lista de posibles errores que pueden darse instanciando un café
        var errores = new List<string>();
        
        if (cafe.Cantidad < 0)
        {
            errores.Add("La cantidad no puede ser menor que 0");
        }
        if (cafe.Puntuacion < 7 || cafe.Puntuacion > 10)
        {
            errores.Add("La puntuación debe estar entre 7 y 10");
        }
        if (!Enum.IsDefined(typeof(TipoOrigen), cafe.Origen))
        {
            errores.Add("El campo origen no es válido.");
        }
        if (!Enum.IsDefined(typeof(TipoProceso), cafe.Proceso))
        {
            errores.Add("El campo Proceso no es válido.");
        }
        if (!Enum.IsDefined(typeof(TipoVariedad), cafe.Variedad))
        {
            errores.Add("La variedad no es válida");
        }
        return errores;
    }
}