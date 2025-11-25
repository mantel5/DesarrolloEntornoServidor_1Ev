using SuplementosAPI.Models;
using SuplementosAPI.QueryParams; 

namespace SuplementosAPI.Repositories
{
    public interface ICreatinaRepository
    {
        
        Task<List<Creatina>> GetAllAsync(QueryParamsCreatina filtros);
        
        Task<Creatina?> GetByIdAsync(int id);
        Task AddAsync(Creatina creatina);
    }
}