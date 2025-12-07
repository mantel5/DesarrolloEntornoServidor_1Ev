using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Create([FromBody] PedidoCreateDto dto)
        {
            try
            {
                var pedido = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
            {
                return NotFound($"No se encontró el pedido con ID {id}");
            }

            return Ok(pedido);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetByUsuario(int usuarioId)
        {
            var pedidos = await _service.GetByUsuarioIdAsync(usuarioId);
            return Ok(pedidos);
        }
    }
}