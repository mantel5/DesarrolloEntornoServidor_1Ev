namespace SuplementosAPI.Dtos
{
    public class UsuarioViewDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Direccion { get; set; }
        // ¡Aquí NO ponemos la contraseña!
    }
}