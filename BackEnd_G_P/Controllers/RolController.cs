using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] RolDto dto)
        {
            try
            {
                var creado = await _rolService.CrearAsync(dto);
                return Ok(new
                {
                    message = "Rol creado con éxito",
                    creado.Id,
                    creado.Nombre
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var roles = await _rolService.ObtenerTodosAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener roles", Error = ex.Message });
            }
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] Rol rol)
        {
            try
            {
                var editado = await _rolService.EditarAsync(rol);
                return Ok(new
                {
                    message = "Rol actualizado con éxito",
                    editado.Id,
                    editado.Nombre
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _rolService.EliminarAsync(id);
                return Ok(new { message = "Rol eliminado correctamente", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
