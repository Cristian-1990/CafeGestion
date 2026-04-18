namespace CafeGestion.Validators;

public interface IValidador <T>
{
    IEnumerable<string> Validar(T cafe);
}