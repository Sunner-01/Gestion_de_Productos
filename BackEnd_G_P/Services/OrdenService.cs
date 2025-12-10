using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class OrdenService
    {
        private readonly AppDbContext _context;
        private readonly MovimientoInventarioService _movimientoService;

        public OrdenService(AppDbContext context, MovimientoInventarioService movimientoService)
        {
            _context = context;
            _movimientoService = movimientoService;
        }

        // Crear orden (Venta)
        public async Task<Orden> CrearAsync(CrearOrdenDto dto)
        {
            // Validar usuario
            if (!await _context.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId))
                throw new Exception("Usuario no encontrado.");

            var orden = new Orden
            {
                UsuarioId = dto.UsuarioId,
                Fecha = DateTime.UtcNow
            };

            decimal totalOrden = 0;

            foreach (var item in dto.Productos)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId)
                    ?? throw new Exception($"Producto con ID {item.ProductoId} no encontrado.");

                // Registrar movimiento de salida
                var movimientoDto = new Movimiento_InventarioDto
                {
                    ProductoId = item.ProductoId,
                    Tipo = "Salida",
                    Cantidad = item.Cantidad,
                    Motivo = "Venta"
                };

                await _movimientoService.RegistrarAsync(movimientoDto);

                // Agregar detalle a la orden
                orden.Productos.Add(new OrdenProducto
                {
                    ProductoId = producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                });

                totalOrden += item.Cantidad * producto.Precio;
            }

            orden.Total = totalOrden;

            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();

            // Recargar la orden con los datos del Usuario
            var ordenCompleta = await _context.Ordenes
                .Include(o => o.Usuario)
                .Include(o => o.Productos)
                .FirstOrDefaultAsync(o => o.Id == orden.Id);

            return ordenCompleta ?? orden;
        }

        // Obtener una orden con detalle completo
        public async Task<OrdenDetalleDto> ObtenerDetalleAsync(int id)
        {
            var orden = await _context.Ordenes
                .Include(o => o.Productos)
                .ThenInclude(op => op.Producto)
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new Exception("Orden no encontrada.");

            return new OrdenDetalleDto
            {
                Id = orden.Id,
                Fecha = orden.Fecha,
                Productos = orden.Productos.Select(op => new OrdenProductoDetalleDto
                {
                    NombreProducto = op.Producto.Nombre,
                    Cantidad = op.Cantidad,
                    PrecioUnitario = op.PrecioUnitario
                }).ToList()
            };
        }

        // Listar órdenes
        public async Task<List<OrdenDetalleDto>> ListarAsync()
        {
            var ordenes = await _context.Ordenes
                .Include(o => o.Productos)
                .ThenInclude(op => op.Producto)
                .OrderByDescending(o => o.Fecha)
                .ToListAsync();

            return ordenes.Select(o => new OrdenDetalleDto
            {
                Id = o.Id,
                Fecha = o.Fecha,
                Productos = o.Productos.Select(op => new OrdenProductoDetalleDto
                {
                    NombreProducto = op.Producto.Nombre,
                    Cantidad = op.Cantidad,
                    PrecioUnitario = op.PrecioUnitario
                }).ToList()
            }).ToList();
        }
    }
}
