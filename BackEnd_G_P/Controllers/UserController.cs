using BackEnd_G_P.Models;
using BackEnd_G_P.Models.DTOs;
using BackEnd_G_P.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_G_P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UserRegDto dto)
        {
            try
            {
                var creado = await _userService.RegistrarAsync(dto);
                return Ok(new
                {
                    message = "Usuario creado con éxito",
                    creado.Id,
                    creado.NombreUsuario,
                    creado.Email,
                    creado.RolId,
                    creado.EstaActivo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var usuario = await _userService.LoginAsync(request.NombreUsuario, request.Password);
                return Ok(new
                {
                    message = "Login exitoso",
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.Email,
                    usuario.RolId,
                    usuario.EstaActivo
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPut("estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                await _userService.CambiarEstadoAsync(id);
                return Ok(new { message = "Estado del usuario cambiado correctamente" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var usuarios = await _userService.ObtenerTodosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener usuarios", Error = ex.Message });
            }
        }

        [HttpPut("password/{id}")]
        public async Task<IActionResult> CambiarPassword(int id, [FromBody] CambioPasswordRequest request)
        {
            try
            {
                await _userService.CambiarPasswordAsync(id, request.PasswordActual, request.PasswordNueva);
                return Ok(new { message = "Contraseña cambiada con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

    // DTOs simples (como tú los usas)
    public class LoginRequest
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CambioPasswordRequest
    {
        public string PasswordActual { get; set; } = string.Empty;
        public string PasswordNueva { get; set; } = string.Empty;
    }
}
