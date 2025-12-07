namespace SuplementosAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Confirmado";

        // Relación: Un pedido tiene muchas líneas
        public List<LineaPedido> Lineas { get; set; } = new List<LineaPedido>();
    }
}