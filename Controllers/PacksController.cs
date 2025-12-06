using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacksController : ControllerBase
    {
        private readonly IPacksService _service;

        public PacksController(IPacksService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Packs>>> GetAll([FromQuery] QueryParamsPacks filtros)
        {
            var lista = await _service.GetAllAsync(filtros);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Packs>> GetById([FromRoute] int id)
        {
            try
            {
                var producto = await _service.GetByIdAsync(id);
                if (producto == null) return NotFound();
                return Ok(producto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pack encontrado, pero uno o más componentes no existen.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Packs>> Create([FromBody] PacksCreateDto dto)
        {
            try
            {
                var nuevo = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PacksCreateDto dto)
        {
             try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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