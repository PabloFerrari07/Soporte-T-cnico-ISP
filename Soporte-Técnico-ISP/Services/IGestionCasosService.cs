using Soporte_Técnico_ISP.Dtos;

namespace Soporte_Técnico_ISP.Services
{
    public interface IGestionCasosService
    {
        Task<IEnumerable<CasoDto>> ObtenerCasos();
        Task<CasoDto> ObtenerCasoPorId(int id);
        Task<CasoDto> AgregarCaso(CasoDto dto);
        Task<CasoDto> EditarCaso(int id, CasoDto dto);
        Task EliminarCaso(int id);
    }
}
