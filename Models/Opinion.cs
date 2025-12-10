namespace SuplementosAPI.Models
{
    public class Opinion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty; 
        public int ProductoId { get; set; }
        public string Texto { get; set; } = string.Empty; 
        public int Puntuacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}