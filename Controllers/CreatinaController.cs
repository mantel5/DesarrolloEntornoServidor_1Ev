using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatinaController : ControllerBase
    {
        private readonly ICreatinaService _service;

        // Inyección de Dependencias: Pedimos el Servicio
        public CreatinaController(ICreatinaService service)
        {
            _service = service;
        }

        // GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creatina>>> GetAll(
            [FromQuery] QueryParamsCreatina filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        // GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<Creatina>> GetById([FromRoute] int id)
        {
            var creatina = await _service.GetByIdAsync(id);

            if (creatina == null)
            {
                return NotFound($"No se encontró ninguna creatina con ID {id}");
            }

            return Ok(creatina);
        }

        // Post
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Creatina>> Create([FromBody] CreatinaCreateDto dto)
        {
            try
            {
                var nuevaCreatina = await _service.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = nuevaCreatina.Id }, nuevaCreatina);
            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content (Borrado con éxito)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}
