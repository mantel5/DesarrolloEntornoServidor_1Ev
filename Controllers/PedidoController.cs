using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuplementosAPI.DTOs;
using SuplementosAPI.Services;

namespace SuplementosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // <--- Candado General (Nadie entra sin token)
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        // GET: Solo Admin ve todo el historial
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // POST: Cualquiera logueado puede comprar
        [HttpPost]
        public async Task<ActionResult> Create(PedidoDto pedidoDto)
        {
            await _service.AddAsync(pedidoDto);
            return Ok(new { message = "Pedido realizado con éxito" });
        }
    }
}