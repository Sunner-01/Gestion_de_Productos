using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class ProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        // Crear producto
        public async Task<Producto> CrearAsync(ProductoDto dto)
        {
            // Validar que exista la categoría
            if (!await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId))
                throw new Exception("La categoría seleccionada no existe.");

            // Validar que exista el proveedor
            if (!await _context.Proveedores.AnyAsync(p => p.Id == dto.ProveedorId))
                throw new Exception("El proveedor seleccionado no existe.");

            // Validar nombre único
            if (await _context.Productos.AnyAsync(p => p.Nombre == dto.Nombre))
                throw new Exception("Ya existe un producto con ese nombre.");

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion=dto.Descripcion,
                Precio = dto.Precio,
                ProveedorId = dto.ProveedorId,
                CategoriaId = dto.CategoriaId,
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        // Listar todos los productos
        public async Task<List<object>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.StockActual,
                    CategoriaId = p.CategoriaId,
                    CategoriaNombre = p.Categoria.Nombre,
                    ProveedorId = p.ProveedorId,
                    ProveedorNombre = p.Proveedor.Nombre
                })
                .OrderBy(p => p.Nombre)
                .ToListAsync<object>();
        }

        // Listar productos por categoría
        public async Task<List<object>> ObtenerPorCategoriaAsync(int categoriaId)
        {
            if (!await _context.Categorias.AnyAsync(c => c.Id == categoriaId))
                throw new Exception("La categoría no existe.");

            return await _context.Productos
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.StockActual,
                    CategoriaNombre = p.Categoria.Nombre,
                    ProveedorNombre = p.Proveedor.Nombre
                })
                .OrderBy(p => p.Nombre)
                .ToListAsync<object>();
        }

        // Editar producto
        public async Task<Producto> EditarAsync(Producto productoActualizado)
        {
            var producto = await _context.Productos.FindAsync(productoActualizado.Id)
                ?? throw new Exception("Producto no encontrado.");

            if (!await _context.Categorias.AnyAsync(c => c.Id == productoActualizado.CategoriaId))
                throw new Exception("La categoría seleccionada no existe.");

            if (!await _context.Proveedores.AnyAsync(p => p.Id == productoActualizado.ProveedorId))
                throw new Exception("El proveedor seleccionado no existe.");

            // Validar nombre único
            if (await _context.Productos.AnyAsync(p => p.Nombre == productoActualizado.Nombre && p.Id != producto.Id))
                throw new Exception("Ya existe otro producto con ese nombre.");

            producto.Nombre = productoActualizado.Nombre;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.CategoriaId = productoActualizado.CategoriaId;
            producto.ProveedorId = productoActualizado.ProveedorId;

            await _context.SaveChangesAsync();
            return producto;
        }

        // Eliminar producto
        public async Task EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id)
                ?? throw new Exception("Producto no encontrado.");

            if (await _context.MovimientosInventario.AnyAsync(m => m.ProductoId == id))
                throw new Exception("No se puede eliminar el producto porque tiene movimientos de inventario registrados.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        // Obtener un producto por Id
        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception("Producto no encontrado.");
        }
    }
}
