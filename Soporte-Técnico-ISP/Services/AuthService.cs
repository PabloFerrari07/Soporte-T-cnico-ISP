using Microsoft.EntityFrameworkCore;
using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;

namespace Soporte_Técnico_ISP.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;

        }
        public async Task<UsuarioDto> RegistrarUsuario(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.rol))
            {
                dto.rol = "cliente";
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.passwordHash);

            var usuario = new Usuario
            {
                nombre = dto.nombre,
                email = dto.email,
                passwordHash = passwordHash,
                rol = dto.rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                nombre = usuario.nombre,
                email = usuario.email,
                rol = usuario.rol
            };

            return usuarioDto;

        }
        public async Task<UsuarioDto> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == dto.email);

            if (usuario == null)
            {
                return null;
            }

            bool passwordValida = BCrypt.Net.BCrypt.Verify(dto.passwordHash, usuario.passwordHash);

            if (!passwordValida)
            {
                return null;
            }

            string token = _tokenService.GenerarToken(usuario);

            return new UsuarioDto
            {
                nombre = usuario.nombre,
                email = usuario.email,
                rol = usuario.rol,
                Token = token
            };
        }



    }
}
