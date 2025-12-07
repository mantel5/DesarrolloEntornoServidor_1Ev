using SuplementosAPI.Dtos;
using SuplementosAPI.Models;

namespace SuplementosAPI.Services
{
    public interface IPedidoService
    {
        Task<Pedido> CreateAsync(PedidoCreateDto dto);
        Task<Pedido?> GetByIdAsync(int id);
        Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId);
    }
}