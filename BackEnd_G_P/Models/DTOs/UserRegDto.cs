using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models.DTOs
{
    public class UserRegDto
    {

        [Required, MaxLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RolId { get; set; } = 1;
       
    }
}
