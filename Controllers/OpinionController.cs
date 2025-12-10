using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _service;

        public OpinionController(IOpinionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OpinionCreateDto dto)
        {
            var opinion = await _service.CreateAsync(dto);
            return StatusCode(201, opinion);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OpinionQueryParams queryParams)
        {
            var opiniones = await _service.GetAllAsync(queryParams);
            return Ok(opiniones);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}