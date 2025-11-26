using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProteinaController : ControllerBase
    {
        private readonly IProteinaService _service;

        public ProteinaController(IProteinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proteina>>> GetAll([FromQuery] QueryParamsProteina filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proteina>> GetById(int id)
        {
            var proteina = await _service.GetByIdAsync(id);
            if (proteina == null) return NotFound();
            return Ok(proteina);
        }

        [HttpPost]
        public async Task<ActionResult<Proteina>> Create([FromBody] ProteinaCreateDto dto)
        {
            try
            {
                var nueva = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            { 
                await _service.DeleteAsync(id); 
                return NoContent(); 
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}