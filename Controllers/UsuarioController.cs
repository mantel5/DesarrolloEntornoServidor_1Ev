using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UsuarioViewDto>> Register([FromBody] UsuarioRegisterDto dto)
        {
            try
            {
                var usuarioCreado = await _service.RegisterAsync(dto);
                
                return CreatedAtAction(nameof(GetById), new { id = usuarioCreado.Id }, usuarioCreado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioViewDto>> Login([FromBody] UsuarioLoginDto dto)
        {
            var usuario = await _service.LoginAsync(dto);

            if (usuario == null)
            {
                // 401 Unauthorized es el código estándar para "Login fallido"
                return Unauthorized("Email o contraseña incorrectos.");
            }

            // Si el login es correcto, devolvemos los datos del usuario (sin password)
            return Ok(usuario);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioViewDto>> GetById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }

            return Ok(usuario);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioViewDto>>> GetAll(
            [FromQuery] QueryParamsUsuario filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }
    }
}