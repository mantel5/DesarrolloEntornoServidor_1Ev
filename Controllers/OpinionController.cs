using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.DTOs;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _service;

        public OpinionController(IOpinionService service)
        {
            _service = service;
        }

        // ...

        [HttpGet]
        // Aquí devolvemos OpinionDto (porque queremos mostrar el ID y la Fecha)
        public async Task<ActionResult<IEnumerable<OpinionDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        [Authorize] 
        // 👇 AQUÍ CAMBIAMOS: Recibimos OpinionCreateDto
        public async Task<ActionResult> Create(OpinionCreateDto opinionDto)
        {
            await _service.AddAsync(opinionDto);
            return Ok();
        }
        
        // ...

        // 👮‍♂️ SOLO EL ADMIN PUEDE BORRAR UNA OPINIÓN (Moderación)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}