using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        // Crear categoría
        public async Task<Categoria> CrearAsync(CategoriaDto dto)
        {
            if (await _context.Categorias.AnyAsync(c => c.Nombre == dto.Nombre))
                throw new Exception("Ya existe una categoría con ese nombre.");
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion=dto.Descripcion,
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Listar todas las categorías
        public async Task<List<Categoria>> ObtenerTodasAsync()
        {
            return await _context.Categorias
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        // Editar categoría
        public async Task<Categoria> EditarAsync(Categoria categoriaActualizada)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaActualizada.Id)
                ?? throw new Exception("Categoría no encontrada.");

            if (await _context.Categorias.AnyAsync(c => c.Nombre == categoriaActualizada.Nombre && c.Id != categoriaActualizada.Id))
                throw new Exception("Ya existe otra categoría con ese nombre.");

            categoria.Nombre = categoriaActualizada.Nombre;
            categoria.Descripcion = categoriaActualizada.Descripcion;

            await _context.SaveChangesAsync();
            return categoria;
        }

        // Eliminar categoría
        public async Task EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id)
                ?? throw new Exception("Categoría no encontrada.");

            // Verificar si hay productos que usan esta categoría
            if (await _context.Productos.AnyAsync(p => p.CategoriaId == id))
                throw new Exception("No se puede eliminar la categoría porque tiene productos asociados.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        // Obtener una categoría por Id
        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id)
                ?? throw new Exception("Categoría no encontrada.");
        }
    }
}
