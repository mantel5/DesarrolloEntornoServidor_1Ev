using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IProteinaRepository
    {
        Task AddAsync(Proteina proteina);
        Task<Proteina?> GetByIdAsync(int id);
        Task<List<Proteina>> GetAllAsync(QueryParamsProteina filtros);
        Task UpdateAsync(Proteina proteina);
        Task DeleteAsync(int id);
    }
}