using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Movimiento_Inventario
    {
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required, MaxLength(10)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [MaxLength(300)]
        public string? Motivo { get; set; }
        public virtual Producto Producto { get; set; } = null!;
    }
}
