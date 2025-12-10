namespace BackEnd_G_P.Models.DTOs
{
    public class OrdenDetalleDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public List<OrdenProductoDetalleDto> Productos { get; set; } = new();

        public decimal Total => Productos.Sum(p => p.Subtotal);
    }
}
