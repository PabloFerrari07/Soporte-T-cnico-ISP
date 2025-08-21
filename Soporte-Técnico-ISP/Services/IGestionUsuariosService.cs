using Soporte_Técnico_ISP.Dtos;

namespace Soporte_Técnico_ISP.Services
{
    public interface IGestionUsuariosService
    {
        Task<IEnumerable<UsuarioDto>> ObtenerUsuarios();
        Task<IEnumerable<UsuarioDto>> ObtenerUsuariosPorId(int id);
        Task<UsuarioDto> AgregarUsuarios(UsuarioDto dto);

        Task<UsuarioDto> EditarUsuario(int id, UsuarioDto dto);

        Task EliminarUsuarios(int id);

    }
}
