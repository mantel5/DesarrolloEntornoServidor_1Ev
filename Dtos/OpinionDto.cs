namespace SuplementosAPI.DTOs
{
    public class OpinionDto
    {
        public int Id { get; set; }        // <--- El CreateDto no tiene esto
        public string Mensaje { get; set; }
        public int Puntuacion { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; } // <--- El CreateDto no tiene esto
    }
}