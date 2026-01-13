using SuplementosAPI.DTOs;

namespace SuplementosAPI.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> GetAllAsync(); // <--- OJO: Devuelve DTOs, no Modelos
        Task AddAsync(PedidoDto pedidoDto);
    }
}