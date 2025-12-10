using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}
