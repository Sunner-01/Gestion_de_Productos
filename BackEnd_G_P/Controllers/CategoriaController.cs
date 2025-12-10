using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] CategoriaDto dto)
        {
            try
            {
                var creada = await _categoriaService.CrearAsync(dto);
                return Ok(new
                {
                    message = "Categoría creada con éxito",
                    creada.Id,
                    creada.Nombre,
                    creada.Descripcion
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var categorias = await _categoriaService.ObtenerTodasAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener categorías", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var categoria = await _categoriaService.ObtenerPorIdAsync(id);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] Categoria categoria)
        {
            try
            {
                var editada = await _categoriaService.EditarAsync(categoria);
                return Ok(new
                {
                    message = "Categoría actualizada con éxito",
                    editada.Id,
                    editada.Nombre,
                    editada.Descripcion
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
                await _categoriaService.EliminarAsync(id);
                return Ok(new { message = "Categoría eliminada correctamente", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
