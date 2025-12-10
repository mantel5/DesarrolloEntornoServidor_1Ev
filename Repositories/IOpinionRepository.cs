using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IOpinionRepository
    {
        Task AddAsync(Opinion opinion);
        Task<List<Opinion>> GetAllAsync(OpinionQueryParams queryParams);
        Task DeleteAsync(int id);
    }
}