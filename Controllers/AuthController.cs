using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; // <--- Este arregla los errores rojos de "Claim"
using System.Text;
using SuplementosAPI.DTOs;
using SuplementosAPI.Repositories;
using SuplementosAPI.Models;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // 1. BUSCAR AL USUARIO POR EMAIL (Usando tu método optimizado)
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

            // 2. VERIFICAR SI EXISTE Y SI LA CONTRASEÑA COINCIDE
            // (Nota: En un futuro real deberías encriptar la contraseña, pero para clase vale así)
            if (usuario == null || usuario.Password != loginDto.Password)
            {
                return Unauthorized(" Usuario o contraseña incorrectos.");
            }

            // 3. CREAR LOS "CLAIMS" (Los datos del carnet de identidad digital)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre ?? "Usuario"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "User") // Para proteger rutas de Admin
            };

            // 4. GENERAR LA FIRMA DE SEGURIDAD
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 5. FABRICAR EL TOKEN
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                mensaje = "¡Login exitoso! Aquí tienes tu pulsera (token)."
            });
        }
    }
}