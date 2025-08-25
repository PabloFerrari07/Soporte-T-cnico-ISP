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

        [HttpGet]
        public async Task<IActionResult> ObtenerCasos()
        {
            var casos = await _gestionCasosService.ObtenerCasos();
            return Ok(casos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCasoPorId(int id)
        {
            var caso = await _gestionCasosService.ObtenerCasoPorId(id);
            if (caso == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(caso);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCaso([FromBody] CasoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var casoCreado = await _gestionCasosService.AgregarCaso(dto);
            return CreatedAtAction(nameof(ObtenerCasoPorId), new { id = casoCreado.id }, casoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCaso(int id, [FromBody] CasoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var casoEditado = await _gestionCasosService.EditarCaso(id, dto);
            if (casoEditado == null)
                return NotFound($"No se encontró un caso con id {id}");

            return Ok(casoEditado);
        }

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
