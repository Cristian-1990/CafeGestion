using System.Text.Json.Serialization;
namespace CafeGestion.Dto;
/// <summary>
/// Representa los atributos de un objeto a un formato más simple de entender para un JSON.
/// Transforma un objeto en datos legibles en formato json
/// </summary>
/// <remarks>Las propiedades (id,nombre,cantidad...) en minusculas para seguir el estandar camelCase de JSON</remarks>>
public record CafeDto(
    [property:JsonPropertyName("id")] int Id,
    [property:JsonPropertyName("nombre")]string Nombre,
    [property:JsonPropertyName("cantidad")] int Cantidad ,
    [property:JsonPropertyName("puntuacion")]double Puntuacion,
    [property:JsonPropertyName("entrada")]DateTime Entrada,
    [property:JsonPropertyName("disponible")] bool Disponible,
    [property:JsonPropertyName("origen")] string Origen,
    [property:JsonPropertyName("variedad")]string Variedad,
    [property:JsonPropertyName("proceso")]string Proceso,
    [property:JsonPropertyName("region")] string Region,
    [property:JsonPropertyName("notaDeCata")] string NotaDeCata,
    [property:JsonPropertyName("fechaTueste")] DateTime FechaTueste
    );