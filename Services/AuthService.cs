using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; // <--- NECESARIO PARA 'ClaimTypes'
using System.Text;
using SuplementosAPI.DTOs;
using SuplementosAPI.Repositories;
using SuplementosAPI.Models;   // <--- ¡IMPORTANTE! AQUÍ VIVE TU CLASE 'Usuario'

namespace SuplementosAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // 1. Buscamos usuario
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

            // 2. Comprobamos contraseña
            if (usuario == null || usuario.Password != loginDto.Password)
            {
                return null;
            }

            // 3. Generamos el token
            return GenerateToken(usuario);
        }

        // --- MÉTODO PRIVADO ---
        private string GenerateToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Role, usuario.Rol),
                    new Claim(ClaimTypes.Email, usuario.Email),
                }),
                
                Expires = DateTime.UtcNow.AddDays(7),
                
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        // --- MÉTODO NUEVO DE VALIDACIÓN DE PROPIEDAD ---
        public bool HasAccessToResource(int requestedUserId, ClaimsPrincipal user)
        {
            // 1. Buscamos el ID dentro del token de quien hace la petición
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            // Si el token no tiene ID o es raro, fuera.
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return false; 
            }

            // 2. ¿Es el dueño del recurso? (Ej: Pepito(5) quiere editar a Pepito(5))
            var isOwnResource = userId == requestedUserId;

            // 3. ¿Es Admin? (El Admin puede tocarlo todo)
            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && roleClaim.Value == "Admin"; // <--- "Admin" como en tu BD

            // 4. Si es dueño O es admin, pasa. Si no, fuera.
            return isOwnResource || isAdmin;
        }
    }
}