using System.ComponentModel.DataAnnotations;

namespace BackEnd_G_P.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Descripcion { get; set; }
    }
}
