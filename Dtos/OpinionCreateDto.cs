using System.ComponentModel.DataAnnotations;

namespace SuplementosAPI.Dtos
{
    public class OpinionCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string UsuarioNombre { get; set; } = string.Empty; 

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Texto { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}