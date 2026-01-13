namespace SuplementosAPI.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        
        // El ID del usuario que compra
        public int UsuarioId { get; set; }
        
        // Aquí podrías poner una lista de líneas de pedido si la tienes
        // public List<LineaPedidoDto> Lineas { get; set; } 
    }
}