using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class MovimientoInventarioService
    {
        private readonly AppDbContext _context;

        public MovimientoInventarioService(AppDbContext context)
        {
            _context = context;
        }

        // Registrar movimiento
        public async Task<Movimiento_Inventario> RegistrarAsync(Movimiento_InventarioDto dto)
        {
            // Validar que el producto exista
            var producto = await _context.Productos.FindAsync(dto.ProductoId)
                ?? throw new Exception("Producto no encontrado.");

            // Validar tipo
            if (dto.Tipo != "Entrada" && dto.Tipo != "Salida")
                throw new Exception("El tipo debe ser 'Entrada' o 'Salida'.");

            // Validar cantidad positiva
            if (dto.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            // Si es salida verificar que haya stock suficiente
            if (dto.Tipo == "Salida" && producto.StockActual < dto.Cantidad)
                throw new Exception($"Stock insuficiente. Actualmente hay {producto.StockActual} unidades.");

            // Actualizar stock
            if (dto.Tipo == "Entrada")
                producto.StockActual += dto.Cantidad;
            else
                producto.StockActual -= dto.Cantidad;
            var mov_inv = new Movimiento_Inventario
            {
                ProductoId = dto.ProductoId,
                Tipo = dto.Tipo,
                Cantidad = dto.Cantidad,
                Motivo= dto.Motivo,
            };

            _context.MovimientosInventario.Add(mov_inv);
            await _context.SaveChangesAsync();

            return mov_inv;
        }

        // Listar todos los movimientos
        public async Task<List<object>> ObtenerTodosAsync()
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .Select(m => new
                {
                    m.Id,
                    m.ProductoId,
                    ProductoNombre = m.Producto.Nombre,
                    m.Tipo,
                    m.Cantidad,
                    m.Fecha,
                    m.Motivo
                })
                .ToListAsync<object>();
        }

        //movimientos por producto
        public async Task<List<object>> ObtenerPorProductoAsync(int productoId)
        {
            if (!await _context.Productos.AnyAsync(p => p.Id == productoId))
                throw new Exception("Producto no encontrado.");

            return await _context.MovimientosInventario
                .Where(m => m.ProductoId == productoId)
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .Select(m => new
                {
                    m.Id,
                    m.Tipo,
                    m.Cantidad,
                    m.Fecha,
                    m.Motivo
                })
                .ToListAsync<object>();
        }
    }
}

