using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reportesService;

        public ReportesController(ReporteService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("stock-actual")]
        public async Task<IActionResult> StockActual()
        {
            var resultado = await _reportesService.StockActualAsync();
            return Ok(resultado);
        }

        [HttpGet("stock-bajo")]
        public async Task<IActionResult> StockBajo()
        {
            var resultado = await _reportesService.StockBajoAsync();
            return Ok(resultado);
        }

        [HttpGet("valor-inventario")]
        public async Task<IActionResult> ValorInventario()
        {
            var valor = await _reportesService.ValorTotalInventarioAsync();
            return Ok(new { ValorTotalInventario = valor });
        }

        [HttpGet("movimientos-mes")]
        public async Task<IActionResult> MovimientosMes()
        {
            var resultado = await _reportesService.MovimientosDelMesAsync();
            return Ok(resultado);
        }

        [HttpGet("top-entradas")]
        public async Task<IActionResult> TopEntradas()
        {
            var resultado = await _reportesService.TopProductosEntradaAsync();
            return Ok(resultado);
        }

        [HttpGet("top-salidas")]
        public async Task<IActionResult> TopSalidas()
        {
            var resultado = await _reportesService.TopProductosSalidaAsync();
            return Ok(resultado);
        }
    }
}
