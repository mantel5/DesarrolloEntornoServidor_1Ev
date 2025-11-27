using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IBebidaRepository
    {
        Task AddAsync(Bebida bebida);
        Task<Bebida?> GetByIdAsync(int id);
        Task<List<Bebida>> GetAllAsync(QueryParamsBebida filtros);
        Task UpdateAsync(Bebida bebida);
        Task DeleteAsync(int id);
    }
}