using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

    }
}
