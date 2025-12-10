using BackEnd_G_P.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class ReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        //Stock actual de todos los productos
        public async Task<List<object>> StockActualAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    Categoria = p.Categoria.Nombre,
                    Proveedor = p.Proveedor.Nombre,
                    p.Precio,
                    p.StockActual,
                    ValorTotal = p.Precio * p.StockActual
                })
                .OrderBy(p => p.Nombre)
                .ToListAsync<object>();
        }

        //Productos con stock bajo
        public async Task<List<object>> StockBajoAsync(int minimo = 10)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.StockActual < minimo)
                .OrderBy(p => p.StockActual)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    Categoria = p.Categoria.Nombre,
                    p.StockActual,
                    p.Precio
                })
                .ToListAsync<object>();
        }

        //Valor total del inventario
        public async Task<decimal> ValorTotalInventarioAsync()
        {
            return await _context.Productos
                .SumAsync(p => p.Precio * p.StockActual);
        }

        //Movimientos del mes actual
        public async Task<List<object>> MovimientosDelMesAsync()
        {
            var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var finMes = inicioMes.AddMonths(1).AddDays(-1);

            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .Where(m => m.Fecha >= inicioMes && m.Fecha <= finMes)
                .OrderByDescending(m => m.Fecha)
                .Select(m => new
                {
                    m.Id,
                    Producto = m.Producto.Nombre,
                    m.Tipo,
                    m.Cantidad,
                    m.Fecha,
                    m.Motivo
                })
                .ToListAsync<object>();
        }

        //5 productos con más entradas
        public async Task<List<object>> TopProductosEntradaAsync(int cantidad = 5)
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .Where(m => m.Tipo == "Entrada")
                .GroupBy(m => m.Producto.Nombre)
                .Select(g => new
                {
                    Producto = g.Key,
                    TotalEntradas = g.Sum(m => m.Cantidad)
                })
                .OrderByDescending(g => g.TotalEntradas)
                .Take(cantidad)
                .ToListAsync<object>();
        }

        //5 productos con más salidas
        public async Task<List<object>> TopProductosSalidaAsync(int cantidad = 5)
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .Where(m => m.Tipo == "Salida")
                .GroupBy(m => m.Producto.Nombre)
                .Select(g => new
                {
                    Producto = g.Key,
                    TotalSalidas = g.Sum(m => m.Cantidad)
                })
                .OrderByDescending(g => g.TotalSalidas)
                .Take(cantidad)
                .ToListAsync<object>();
        }
    }
}
