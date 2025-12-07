namespace SuplementosAPI.Dtos
{
    public class PedidoCreateDto
    {
        public int UsuarioId { get; set; } 
        public List<ItemCarritoDto> Productos { get; set; } = new List<ItemCarritoDto>();
    }

    public class ItemCarritoDto
    {
        public int ProductoId { get; set; }      
        public string TipoProducto { get; set; } 
        public int Cantidad { get; set; }        
    }
}