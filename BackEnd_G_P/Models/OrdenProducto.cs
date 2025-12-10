using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd_G_P.Models
{
    public class OrdenProducto
    {
        public int Id { get; set; }

        public int OrdenId { get; set; }
        [JsonIgnore]
        public Orden Orden { get; set; }

        public int ProductoId { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

    }
}
