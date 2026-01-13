using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.DTOs;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Inyectamos el Servicio, NO el Repositorio
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);

            if (token == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Devolvemos el token en un objeto JSON
            return Ok(new { token = token });
        }
    }
}