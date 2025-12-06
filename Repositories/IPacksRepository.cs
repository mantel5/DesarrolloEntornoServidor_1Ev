using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuplementosAPI.Repositories
{
    public interface IPacksRepository
    {
        Task<List<Packs>> GetAllAsync(QueryParamsPacks filtros);
        Task<Packs?> GetByIdAsync(int id);
        Task<int> AddAsync(Packs pack);
        Task UpdateAsync(Packs pack);
        Task DeleteAsync(int id);
    }
}
