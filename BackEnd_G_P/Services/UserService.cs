using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd_G_P.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        //Registrar usuario
        public async Task<Usuario> RegistrarAsync(UserRegDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario))
                throw new Exception("El nombre de usuario ya existe.");

            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("El email ya está registrado.");

            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.PasswordHash),
                RolId = dto.RolId,
                EstaActivo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }


        //Iniciar sesion
        public async Task<Usuario> LoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null || !usuario.EstaActivo)
                throw new Exception("Usuario o contraseña incorrectos");

            if (!VerifyPassword(password, usuario.PasswordHash))
                throw new Exception("Usuario o contraseña incorrectos");

            return usuario;
        }

        //Cambiar estado activo/inactivo
        public async Task CambiarEstadoAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new Exception("Usuario no encontrado");

            usuario.EstaActivo = !usuario.EstaActivo;
            await _context.SaveChangesAsync();
        }

        //Obtener todos los usuarios
        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    Email = u.Email,
                    RolId = u.RolId,
                    EstaActivo = u.EstaActivo
                })
                .ToListAsync();
        }

        //Cambiar contraseña
        public async Task CambiarPasswordAsync(int id, string passwordActual, string passwordNueva)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new Exception("Usuario no encontrado");

            if (!VerifyPassword(passwordActual, usuario.PasswordHash))
                throw new Exception("La contraseña actual es incorrecta");

            usuario.PasswordHash = HashPassword(passwordNueva);
            await _context.SaveChangesAsync();
        }

        //Metodos privados para hash
        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
