using SuplementosAPI.Models;

namespace SuplementosAPI.Repositories
{
    public interface IPedidoRepository
    {
        // Añade estas dos líneas:
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task AddAsync(Pedido pedido);
    }
}