using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

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

        // ---------------------------------------------------------
        // 1. GET ALL (LISTA) - CON FILTROS
        // URL Ej: api/Creatina?precioMax=30&soloCreapure=true
        // ---------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creatina>>> GetAll(
            [FromQuery] QueryParamsCreatina filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        // ---------------------------------------------------------
        // 2. GET BY ID (DETALLE)
        // URL Ej: api/Creatina/5
        // ---------------------------------------------------------
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

        // ---------------------------------------------------------
        // 3. POST (CREAR)
        // Recibe el DTO en el cuerpo del mensaje (JSON)
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Creatina>> Create([FromBody] CreatinaCreateDto dto)
        {
            try
            {
                // Llamamos al servicio.
                // Si el DTO trae datos malos (ej: precio negativo),
                // el CONSTRUCTOR del Modelo lanzará una excepción aquí.
                var nuevaCreatina = await _service.CreateAsync(dto);

                // Devolvemos 201 Created y la URL para ver el producto
                return CreatedAtAction(nameof(GetById), new { id = nuevaCreatina.Id }, nuevaCreatina);
            }
            catch (ArgumentException ex)
            {
                // Capturamos tus validaciones (precio negativo, nombre vacío...)
                // y devolvemos un 400 Bad Request limpio.
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ---------------------------------------------------------
        // 4. DELETE (BORRAR)
        // ---------------------------------------------------------
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
        
        // (Opcional) Podrías añadir aquí el PUT para actualizar
    }
}