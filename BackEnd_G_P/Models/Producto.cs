using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; } = 0;

        [Required]
        public int StockActual { get; set; } = 0;

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int ProveedorId { get; set; }
        public virtual Categoria Categoria { get; set; } = null!;
        public virtual Proveedor Proveedor { get; set; } = null!;
        public List<OrdenProducto> Ordenes { get; set; } = new(); 
    }
}
