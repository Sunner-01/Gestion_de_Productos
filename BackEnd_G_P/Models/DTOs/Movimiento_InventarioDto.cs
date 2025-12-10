using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models.DTOs
{
    public class Movimiento_InventarioDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required, MaxLength(10)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public int Cantidad { get; set; }


        [MaxLength(300)]
        public string? Motivo { get; set; }
    }

}
