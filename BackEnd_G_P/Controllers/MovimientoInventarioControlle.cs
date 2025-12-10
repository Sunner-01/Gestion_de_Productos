using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoInventarioController : ControllerBase
    {
        private readonly MovimientoInventarioService _service;

        public MovimientoInventarioController(MovimientoInventarioService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Movimiento_InventarioDto dto)
        {
            try
            {
                var registrado = await _service.RegistrarAsync(dto);
                return Ok(new
                {
                    message = $"Movimiento de {dto.Tipo} registrado con éxito",
                    registrado.Id,
                    registrado.Tipo,
                    registrado.Cantidad,
                    registrado.Fecha
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
                var movimientos = await _service.ObtenerTodosAsync();
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener movimientos", Error = ex.Message });
            }
        }

        [HttpGet("por-producto/{productoId}")]
        public async Task<IActionResult> ObtenerPorProducto(int productoId)
        {
            try
            {
                var movimientos = await _service.ObtenerPorProductoAsync(productoId);
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
