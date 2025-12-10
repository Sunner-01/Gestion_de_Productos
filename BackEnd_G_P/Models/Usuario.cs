using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RolId { get; set; } = 1;
        [Required]
        public bool EstaActivo { get; set; } = true;
    }
}
