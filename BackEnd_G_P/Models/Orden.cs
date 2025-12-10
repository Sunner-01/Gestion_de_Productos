namespace BackEnd_G_P.Models
{
    public class Orden
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        
        public decimal Total { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public List<OrdenProducto> Productos { get; set; } = new();
    }
}
