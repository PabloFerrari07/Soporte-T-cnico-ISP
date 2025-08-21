using Soporte_Técnico_ISP.Dtos;

namespace Soporte_Técnico_ISP.Services
{
    public interface IAuthService
    {
        Task<UsuarioDto> RegistrarUsuario(RegisterDto dto);

        Task<UsuarioDto> Login(LoginDto dto);
    }
}
