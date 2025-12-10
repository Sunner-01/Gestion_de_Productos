namespace BackEnd_G_P.Models.DTOs
{
    public class CrearOrdenDto
    {
        public int UsuarioId { get; set; }
        public List<ItemOrdenDto> Productos { get; set; } = new();
    }
}
