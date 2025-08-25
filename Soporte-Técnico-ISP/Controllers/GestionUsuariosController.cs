using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soporte_Técnico_ISP.Dtos;
using Soporte_Técnico_ISP.Models;
using Soporte_Técnico_ISP.Services;

namespace Soporte_Técnico_ISP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GestionUsuariosController : ControllerBase
    {
        private readonly IGestionUsuariosService _gestionUsuariosSerivice;
        public GestionUsuariosController(IGestionUsuariosService gestionUsuariosSerivice)
        {
       
            _gestionUsuariosSerivice = gestionUsuariosSerivice;
        }

        [HttpPost]
        [Route("AgregarUsuarios")]
        public async Task<ActionResult<UsuarioDto>> agregarUsuario(UsuarioDto dto)
        {
            var usuario = await _gestionUsuariosSerivice.AgregarUsuarios(dto);

            return Ok(usuario);
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<IEnumerable<UsuarioDto>> Obtener() => await _gestionUsuariosSerivice.ObtenerUsuarios();

        [HttpPut]
        public async Task<ActionResult<UsuarioDto>> PutUsuario(int id, [FromBody] UsuarioDto dto)
        {
            var usuarioEditado = await _gestionUsuariosSerivice.EditarUsuario(id, dto);
            return Ok(usuarioEditado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            await _gestionUsuariosSerivice.EliminarUsuarios(id);
            return NoContent();
        }

    }
}
