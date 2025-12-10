using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Services
{
    public class RolService
    {
        private readonly AppDbContext _context;

        public RolService(AppDbContext context)
        {
            _context = context;
        }

        // Crear rol
        public async Task<Rol> CrearAsync(RolDto dto)
        {
            if (await _context.Roles.AnyAsync(r => r.Nombre == dto.Nombre))
                throw new Exception("Ya existe un rol con ese nombre.");
            var rol = new Rol
            {
                Nombre = dto.Nombre,

            };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        // Listar todos los roles
        public async Task<List<Rol>> ObtenerTodosAsync()
        {
            return await _context.Roles
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        // Editar rol
        public async Task<Rol> EditarAsync(Rol rolActualizado)
        {
            var rol = await _context.Roles.FindAsync(rolActualizado.Id)
                ?? throw new Exception("Rol no encontrado.");

            if (await _context.Roles.AnyAsync(r => r.Nombre == rolActualizado.Nombre && r.Id != rolActualizado.Id))
                throw new Exception("Ya existe otro rol con ese nombre.");

            rol.Nombre = rolActualizado.Nombre;
            await _context.SaveChangesAsync();

            return rol;
        }

        // Eliminar rol
        public async Task EliminarAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id)
                ?? throw new Exception("Rol no encontrado.");

            //verificar si hay usuarios con este rol
            if (await _context.Usuarios.AnyAsync(u => u.RolId == id))
                throw new Exception("No se puede eliminar el rol porque hay usuarios asignados.");

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }
    }
}
