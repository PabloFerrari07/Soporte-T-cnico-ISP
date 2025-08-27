using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Services;

namespace Soporte_Técnico_ISP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasosController : ControllerBase
    {
        private readonly IGestionCasosService _gestionCasosService;

        public CasosController(IGestionCasosService gestionCasosService)
        {
            _gestionCasosService = gestionCasosService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCasos()
        {
            var casos = await _gestionCasosService.ObtenerCasos();
            return Ok(casos);
        }

        [Authorize(Roles = "admin,cliente")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCasoPorId(int id)
        {
            var caso = await _gestionCasosService.ObtenerCasoPorId(id);
            if (caso == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(caso);
        }

 
        [Authorize(Roles = "admin,cliente")]
        [HttpPost]
        public async Task<IActionResult> AgregarCaso([FromBody] CasoCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var casoCreado = await _gestionCasosService.AgregarCaso(dto);
            return CreatedAtAction(nameof(ObtenerCasoPorId), new { id = casoCreado.id }, casoCreado);
        }

        [Authorize(Roles = "admin,cliente")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCaso(int id, [FromBody] CasoEditarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var casoEditado = await _gestionCasosService.EditarCaso(id, dto);
            if (casoEditado == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(casoEditado);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoDto dto)
        {
            var caso = await _gestionCasosService.CambiarEstado(id, dto);
            if (caso == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(caso);
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{id}/prioridad")]
        public async Task<IActionResult> CambiarPrioridad(int id, [FromBody] CambiarPrioridadDto dto)
        {
            var caso = await _gestionCasosService.CambiarPrioridad(id, dto);
            if (caso == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(caso);
        }

  
        [Authorize(Roles = "admin,cliente")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCaso(int id)
        {
            var caso = await _gestionCasosService.ObtenerCasoPorId(id);
            if (caso == null)
                return NotFound($"No se encontró un caso con id {id}");

            await _gestionCasosService.EliminarCaso(id);
            return NoContent();
        }
    }
}
