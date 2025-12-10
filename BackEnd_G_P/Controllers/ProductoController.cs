using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] ProductoDto dto)
        {
            try
            {
                var creado = await _productoService.CrearAsync(dto);
                return Ok(new
                {
                    message = "Producto creado con éxito",
                    creado.Id,
                    creado.Nombre,
                    creado.Precio,
                    creado.StockActual
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
                var productos = await _productoService.ObtenerTodosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener productos", Error = ex.Message });
            }
        }

        [HttpGet("por-categoria/{categoriaId}")]
        public async Task<IActionResult> ObtenerPorCategoria(int categoriaId)
        {
            try
            {
                var productos = await _productoService.ObtenerPorCategoriaAsync(categoriaId);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerPorIdAsync(id);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] Producto producto)
        {
            try
            {
                var editado = await _productoService.EditarAsync(producto);
                return Ok(new { message = "Producto actualizado con éxito (stock no modificable)", editado.Id, editado.Nombre });
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
                await _productoService.EliminarAsync(id);
                return Ok(new { message = "Producto eliminado correctamente", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
