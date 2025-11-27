using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TortitasController : ControllerBase
    {
        private readonly ITortitasService _service;

        public TortitasController(ITortitasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tortitas>>> GetAll([FromQuery] QueryParamsTortitas filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tortitas>> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Tortitas>> Create([FromBody] TortitasCreateDto dto)
        {
            try
            {
                var nuevo = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
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