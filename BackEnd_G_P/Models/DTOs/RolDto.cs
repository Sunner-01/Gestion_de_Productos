using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models.DTOs
{
    public class RolDto
    {
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}
