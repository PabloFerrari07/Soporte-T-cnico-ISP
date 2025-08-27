using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Soporte_Técnico_ISP.Services.Constants;

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

        public async Task<CasoDto> AgregarCaso(CasoCrearDto dto)
        {
            var usuarioId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);

            var caso = new Caso
            {
                titulo = dto.titulo,
                descripcion = dto.descripcion,
                estado = string.IsNullOrEmpty(dto.estado) ? EstadosCaso.Pendiente : dto.estado,
                prioridad = string.IsNullOrEmpty(dto.prioridad) ? PrioridadesCaso.Baja : dto.prioridad,
                UsuarioId = usuarioId
            };

            await _context.Casos.AddAsync(caso);
            await _context.SaveChangesAsync();

            return new CasoDto
            {
                id = caso.id,
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado,
                prioridad = caso.prioridad,
                UsuarioId = caso.UsuarioId,
                colorPrioridad = PrioridadesCaso.Color(caso.prioridad)
            };
        }


        public async Task<IEnumerable<CasoDto>> ObtenerCasos()
        {
            var casos = await _context.Casos
                .AsNoTracking()
                .Select(c => new CasoDto
                {
                    id = c.id,
                    titulo = c.titulo,
                    descripcion = c.descripcion,
                    estado = c.estado,
                    prioridad = c.prioridad,
                    UsuarioId = c.UsuarioId,
                    colorPrioridad = PrioridadesCaso.Color(c.prioridad)
                })
                .ToListAsync();

            return casos
                .OrderByDescending(c => PrioridadesCaso.Peso(c.prioridad))
                .ThenByDescending(c => c.id).ToList();
        }


        public async Task<CasoDto> ObtenerCasoPorId(int id)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) return null;

            return new CasoDto
            {
                id = caso.id,
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado,
                prioridad = caso.prioridad,
                UsuarioId = caso.UsuarioId,
                colorPrioridad = PrioridadesCaso.Color(caso.prioridad)
            };
        }


        public async Task<CasoDto> EditarCaso(int id, CasoEditarDto dto)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) return null;

            caso.titulo = dto.titulo;
            caso.descripcion = dto.descripcion;

            _context.Casos.Update(caso);
            await _context.SaveChangesAsync();

            return new CasoDto
            {
                id = caso.id,
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado,
                prioridad = caso.prioridad,
                UsuarioId = caso.UsuarioId,
                colorPrioridad = PrioridadesCaso.Color(caso.prioridad)
            };
        }

    
        public async Task<CasoDto> CambiarEstado(int id, CambiarEstadoDto dto)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) return null;
            
            if (!EstadosCaso.Validos.Contains(dto.estado))
                throw new Exception("Estado inválido");

            caso.estado = dto.estado;

            _context.Casos.Update(caso);
            await _context.SaveChangesAsync();

            return new CasoDto
            {
                id = caso.id,
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado,
                prioridad = caso.prioridad,
                UsuarioId = caso.UsuarioId,
                colorPrioridad = PrioridadesCaso.Color(caso.prioridad)
            };
        }

    
        public async Task<CasoDto> CambiarPrioridad(int id, CambiarPrioridadDto dto)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) return null;

            if (!PrioridadesCaso.Validas.Contains(dto.prioridad))
                throw new Exception("Prioridad inválida");

            caso.prioridad = dto.prioridad;

            _context.Casos.Update(caso);
            await _context.SaveChangesAsync();

            return new CasoDto
            {
                id = caso.id,
                titulo = caso.titulo,
                descripcion = caso.descripcion,
                estado = caso.estado,
                prioridad = caso.prioridad,
                UsuarioId = caso.UsuarioId,
                colorPrioridad = PrioridadesCaso.Color(caso.prioridad)
            };
        }


        public async Task EliminarCaso(int id)
        {
            var caso = await _context.Casos.FindAsync(id);
            if (caso == null) throw new Exception("Caso no encontrado");

            _context.Casos.Remove(caso);
            await _context.SaveChangesAsync();
        }
    }
  
}
