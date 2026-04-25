using CafeGestion.Models;
using CafeGestion.Dto;
using CafeGestion.Enums;
namespace CafeGestion.Mapper;
/// <summary>
/// Clase statica que contiene dos metodos:
/// 1. Recibe Dto y devuelve Café    --> Origen = Enum.Parse<TipoOrigen>(dto.Origen)
/// 2.Recibe Café y devuelve Dto     --> Origen = cafe.Origen.ToString
/// </summary>
public static class CafeMapper
{
    /// <summary>
    /// Recibe un CafeDto y devuelve un Cafe
    /// </summary>
    /// <remarks>Convertir de String a Enum</remarks>>
    /// <param name="dto">dto a convertir en Objeto</param>
    /// <returns>Cafe</returns>
    public static Cafe ToModel(this CafeDto dto)
    {
        return new Cafe
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Cantidad = dto.Cantidad,
            Puntuacion = dto.Puntuacion,
            Entrada = dto.Entrada,
            Disponible = dto.Disponible,
            Origen = Enum.TryParse(dto.Origen, out TipoOrigen origen) ? origen :TipoOrigen.Desconocido,
            Variedad = Enum.TryParse(dto.Variedad, out TipoVariedad variedad )? variedad : TipoVariedad.Desconocido,
            Proceso = Enum.TryParse(dto.Proceso, out TipoProceso proceso) ? proceso : TipoProceso.Desconocido,
            Region = dto.Region,
            NotaDeCata = dto.NotaDeCata,
            FechaTueste = dto.FechaTueste,
        };
    }
/// <summary>
/// Funcion que recibe un objeto café y crea un CafeDto a partir de él
/// </summary>
/// <remarks>Convertir los enums a string</remarks>>
/// <param name="model">Cafe a convertir en Dto</param>
/// <returns>CafeDto</returns>
    public static CafeDto ToDto(this Cafe model)
    {
        return new CafeDto
        (
            model.Id,
            model.Nombre,
            model.Cantidad,
            model.Puntuacion,
            model.Entrada,
            model.Disponible,
            model.Origen.ToString(),
            model.Variedad.ToString(),
            model.Proceso.ToString(),
            model.Region,
            model.NotaDeCata,
            model.FechaTueste
         );
    }
}
