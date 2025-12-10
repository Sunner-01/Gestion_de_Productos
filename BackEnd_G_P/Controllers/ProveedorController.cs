using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] ProveedorDto dto)
        {
            try
            {
                var creado = await _proveedorService.CrearAsync(dto);
                return Ok(new
                {
                    message = "Proveedor creado con éxito",
                    creado.Id,
                    creado.Nombre,
                    creado.Email,
                    creado.Telefono
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
                var proveedores = await _proveedorService.ObtenerTodosAsync();
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener proveedores", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var proveedor = await _proveedorService.ObtenerPorIdAsync(id);
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] Proveedor proveedor)
        {
            try
            {
                var editado = await _proveedorService.EditarAsync(proveedor);
                return Ok(new
                {
                    message = "Proveedor actualizado con éxito",
                    editado.Id,
                    editado.Nombre,
                    editado.Email
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
                await _proveedorService.EliminarAsync(id);
                return Ok(new { message = "Proveedor eliminado correctamente", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
