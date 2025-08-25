using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Soporte_Técnico_ISP.Services
{
    public class GestionCasosService : IGestionCasosService
    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GestionCasosService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<CasoDto> AgregarCaso(CasoDto dto)
        {
            var usuarioId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);

            var caso = new Caso
            {
                titulo = dto.titulo,
                descripcion = dto.descripcion,
                estado = dto.estado,
                UsuarioId = usuarioId 
            };

            await _context.Casos.AddAsync(caso);
            await _context.SaveChangesAsync();

            return new CasoDto
            {
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado
            };


        }

        public async Task<IEnumerable<CasoDto>> ObtenerCasos() => await _context.Casos.Select(c => new CasoDto { titulo = c.titulo, descripcion = c.descripcion, estado = c.estado }).ToListAsync();


        public async Task<CasoDto> ObtenerCasoPorId(int id)
        {
            var caso = await _context.Casos.FindAsync(id);

            if (caso == null) return null;

            return new CasoDto
            {
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado
            };
        }

        public async Task<CasoDto> EditarCaso(int id, CasoDto dto)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) return null;

            caso.titulo = dto.titulo;
            caso.estado = dto.estado;
            caso.descripcion = dto.descripcion;


             _context.Casos.Update(caso);
           await  _context.SaveChangesAsync();

            return new CasoDto
            {
                titulo = dto.titulo,
                descripcion = dto.descripcion,
                estado = dto.estado
            };
        }



        public async Task EliminarCaso(int id)
        {
            var caso = await _context.Casos.FindAsync(id);

            if(caso == null)
            {
                throw new Exception("Caso no encontrado");

            } ;


            _context.Casos.Remove(caso);
            await _context.SaveChangesAsync();
        }

    }
}
