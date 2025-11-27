using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalsaController : ControllerBase
    {
        private readonly ISalsaService _service;

        // Inyección de dependencias
        public SalsaController(ISalsaService service)
        {
            _service = service;
        }

        // GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salsa>>> GetAll(
            [FromQuery] QueryParamsSalsa filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }
 
        // GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<Salsa>> GetById([FromRoute] int id)
        {
            var salsa = await _service.GetByIdAsync(id);
            
            if (salsa == null)
            {
                return NotFound($"No se encontró la salsa con ID {id}");
            }

            return Ok(salsa);
        }

        // Post
        [HttpPost]
        public async Task<ActionResult<Salsa>> Create([FromBody] SalsaCreateDto dto)
        {
            try
            {
                var nuevaSalsa = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaSalsa.Id }, nuevaSalsa);
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

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
