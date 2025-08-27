using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;

namespace Soporte_Técnico_ISP.Services
{
    public interface IGestionCasosService
    {
        Task<CasoDto> AgregarCaso(CasoCrearDto dto);

        Task<IEnumerable<CasoDto>> ObtenerCasos();

        Task<CasoDto> ObtenerCasoPorId(int id);

        Task<CasoDto> EditarCaso(int id, CasoEditarDto dto);

        Task<CasoDto> CambiarEstado(int id, CambiarEstadoDto dto);

        Task<CasoDto> CambiarPrioridad(int id, CambiarPrioridadDto dto);

        Task EliminarCaso(int id);
    }
}
