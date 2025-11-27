using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface ITortitasRepository
    {
        Task AddAsync(Tortitas tortitas);
        Task<Tortitas?> GetByIdAsync(int id);
        Task<List<Tortitas>> GetAllAsync(QueryParamsTortitas filtros);
        Task UpdateAsync(Tortitas tortitas);
        Task DeleteAsync(int id); 
    }
}