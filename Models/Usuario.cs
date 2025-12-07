namespace SuplementosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Aquí guardaremos el Hash encriptado
        public string Rol { get; set; } = "Cliente"; // "Admin" o "Cliente"
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public Usuario() { }

        public Usuario(string nombre, string email, string password, string rol, string direccion, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("El email es obligatorio.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("La contraseña es obligatoria.");
            
            // Validación de rol
            if (rol != "Admin" && rol != "Cliente") throw new ArgumentException("Rol inválido.");

            Nombre = nombre;
            Email = email;
            Password = password;
            Rol = rol;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}