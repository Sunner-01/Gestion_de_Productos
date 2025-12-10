using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models.DTOs
{
    public class OrdenCompraDto
    {

        [Required]
        public int Cantidad { get; set; } = 0;
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int ProductoId { get; set; }
    }
}
