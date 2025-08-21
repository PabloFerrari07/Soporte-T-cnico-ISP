using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;
using Soporte_Técnico_ISP.Services;

namespace Soporte_Técnico_ISP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext context, IAuthService authService)
        {
            _authService = authService;
            _context = context;
        }


        [HttpPost]
        [Route("Registro")]
        public async Task<ActionResult<UsuarioDto>> RegistrarUsuario(RegisterDto dto)
        {
            var usuarioDto = await _authService.RegistrarUsuario(dto);
            return Ok(usuarioDto);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginUsuario(LoginDto dto)
        {
            var usuario = await _authService.Login(dto);
            if(usuario == null)
            {
                return Unauthorized("Email o contraseñas incorrectos.");
            }

            return Ok(usuario);
        }
    }
}
