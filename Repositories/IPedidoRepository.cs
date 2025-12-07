using SuplementosAPI.Models;

namespace SuplementosAPI.Repositories
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        
        Task<Pedido?> GetByIdAsync(int id);
        
        Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId);
    }
}