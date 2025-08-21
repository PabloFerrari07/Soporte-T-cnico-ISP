using Microsoft.EntityFrameworkCore;
using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;

namespace Soporte_Técnico_ISP.Services
{
    public class GestionUsuariosService : IGestionUsuariosService
    {
        private readonly AppDbContext _context;

        public GestionUsuariosService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<UsuarioDto>> ObtenerUsuarios()
            =>
            await _context.Usuarios.Select(u => new UsuarioDto { nombre = u.nombre, email = u.email, rol = u.rol }).ToListAsync();
      
        public async Task <UsuarioDto> AgregarUsuarios(UsuarioDto dto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.passwordHash);
            var usuario = new Usuario
            {
                nombre = dto.nombre,
                email = dto.email,
                passwordHash = passwordHash,
                rol = dto.rol
            };


            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                nombre = usuario.nombre,
                email = usuario.email,
                rol = usuario.rol
            };
        }

        public Task<UsuarioDto> EditarUsuario(int id, UsuarioDto dto)
        {
            throw new NotImplementedException();
        }

        public Task EliminarUsuarios(int id)
        {
            throw new NotImplementedException();
        }



        public Task<IEnumerable<UsuarioDto>> ObtenerUsuariosPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
