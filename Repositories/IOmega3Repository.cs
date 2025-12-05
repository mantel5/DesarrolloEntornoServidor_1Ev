using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IOmega3Repository
    {
        Task AddAsync(Omega3 omega);
        Task<Omega3?> GetByIdAsync(int id);
        
        Task<List<Omega3>> GetAllAsync(QueryParamsOmega3 filtros);
        
        Task UpdateAsync(Omega3 omega);
        Task DeleteAsync(int id);
    }
}