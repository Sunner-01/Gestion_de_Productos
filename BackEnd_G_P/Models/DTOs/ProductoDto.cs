using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models.DTOs
{
    public class ProductoDto
    {

        [Required, MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; } = 0;

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int ProveedorId { get; set; }
    }
}
