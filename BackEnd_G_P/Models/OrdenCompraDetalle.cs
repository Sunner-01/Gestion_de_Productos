using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class OrdenCompraDetalle
    {
        public int Id { get; set; }

        [Required]
        public int OrdenCompraId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public decimal SubTotal { get; set; }
        public virtual Producto Producto { get; set; } = null!;
        public virtual OrdenProducto OrdenCompra { get; set; } = null!;
       
    }
}
