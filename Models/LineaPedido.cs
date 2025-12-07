namespace SuplementosAPI.Models
{
    public class LineaPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        
        public string ProductoNombre { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; } 

        public int ProductoIdOriginal { get; set; } 
        public string TipoProductoOriginal { get; set; } = ""; 
    }
}