using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BebidaController : ControllerBase
    {
        private readonly IBebidaService _service;

        public BebidaController(IBebidaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bebida>>> GetAll([FromQuery] QueryParamsBebida filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bebida>> GetById([FromRoute] int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Bebida>> Create([FromBody] BebidaCreateDto dto)
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