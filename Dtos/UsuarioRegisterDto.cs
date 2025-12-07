using System.ComponentModel.DataAnnotations;

namespace SuplementosAPI.Dtos
{
    public class UsuarioRegisterDto
    {
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        [EmailAddress] 
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)] 
        public string Password { get; set; }
        
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        
    }
}