using SuplementosAPI.DTOs;
using SuplementosAPI.Models;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PedidoDto>> GetAllAsync()
        {
            // 1. Traemos los datos crudos de la BD
            var pedidos = await _repository.GetAllAsync();

            // 2. Los convertimos a DTO (La "caja bonita" para el usuario)
            return pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                Fecha = p.Fecha,
                Total = p.Total,
                UsuarioId = p.UsuarioId
            });
        }

        public async Task AddAsync(PedidoDto pedidoDto)
        {
            // 1. Convertimos el DTO a Modelo de Base de Datos
            var pedido = new Pedido
            {
                Fecha = DateTime.UtcNow, // La fecha la ponemos nosotros automática
                Total = pedidoDto.Total,
                UsuarioId = pedidoDto.UsuarioId
            };

            // 2. Guardamos
            await _repository.AddAsync(pedido);
        }
    }
}