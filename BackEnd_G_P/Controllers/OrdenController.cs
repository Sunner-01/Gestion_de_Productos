using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenService _service;

        public OrdenController(OrdenService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearOrdenDto dto)
        {
            try
            {
                var data = await _service.CrearAsync(dto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                var data = await _service.ObtenerDetalleAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var data = await _service.ListarAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener órdenes", Error = ex.Message });
            }
        }
    }
}

