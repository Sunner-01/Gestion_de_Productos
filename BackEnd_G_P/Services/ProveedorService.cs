using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class ProveedorService
    {
        private readonly AppDbContext _context;

        public ProveedorService(AppDbContext context)
        {
            _context = context;
        }

        // Crear proveedor
        public async Task<Proveedor> CrearAsync(ProveedorDto dto)
        {
            // Validar que no exista otro con el mismo nombre o email
            if (await _context.Proveedores.AnyAsync(p => p.Nombre == dto.Nombre))
                throw new Exception("Ya existe un proveedor con ese nombre.");

            if (!string.IsNullOrEmpty(dto.Email) &&
                await _context.Proveedores.AnyAsync(p => p.Email == dto.Email))
                throw new Exception("Ya existe un proveedor con ese email.");
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono= dto.Telefono,
                Email = dto.Email,
                Direccion= dto.Direccion,


            }; 
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        // Listar todos los proveedores
        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            return await _context.Proveedores
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        // Editar proveedor
        public async Task<Proveedor> EditarAsync(Proveedor proveedorActualizado)
        {
            var proveedor = await _context.Proveedores.FindAsync(proveedorActualizado.Id)
                ?? throw new Exception("Proveedor no encontrado.");

            // Validar nombre
            if (await _context.Proveedores.AnyAsync(p => p.Nombre == proveedorActualizado.Nombre && p.Id != proveedorActualizado.Id))
                throw new Exception("Ya existe otro proveedor con ese nombre.");

            // Validar email
            if (!string.IsNullOrEmpty(proveedorActualizado.Email) &&
                await _context.Proveedores.AnyAsync(p => p.Email == proveedorActualizado.Email && p.Id != proveedorActualizado.Id))
                throw new Exception("Ya existe otro proveedor con ese email.");

            proveedor.Nombre = proveedorActualizado.Nombre;
            proveedor.Direccion = proveedorActualizado.Direccion;
            proveedor.Telefono = proveedorActualizado.Telefono;
            proveedor.Email = proveedorActualizado.Email;

            await _context.SaveChangesAsync();
            return proveedor;
        }

        // Eliminar proveedor
        public async Task EliminarAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id)
                ?? throw new Exception("Proveedor no encontrado.");

            //Verificar si tiene productos asociados
            if (await _context.Productos.AnyAsync(p => p.ProveedorId == id))
                throw new Exception("No se puede eliminar el proveedor porque tiene productos asociados.");

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
        }

        // Obtener un proveedor por Id
        public async Task<Proveedor> ObtenerPorIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id)
                ?? throw new Exception("Proveedor no encontrado.");
        }
    }
}
