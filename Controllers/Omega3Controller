using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Omega3Controller : ControllerBase
    {
        private readonly IOmega3Service _service;

        public Omega3Controller(IOmega3Service service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Omega3>>> GetAll(
            [FromQuery] QueryParamsOmega3 filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Omega3>> GetById([FromRoute] int id)
        {
            var producto = await _service.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound($"No se encontró ningún Omega 3 con ID {id}");
            }

            return Ok(producto);
        }


        [HttpPost]
        public async Task<ActionResult<Omega3>> Create([FromBody] Omega3CreateDto dto)
        {
            try
            {
c.
                var nuevo = await _service.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
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