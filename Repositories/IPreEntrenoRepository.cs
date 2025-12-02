using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IPreEntrenoRepository
    {
        Task AddAsync(PreEntreno preEntreno);
        Task<PreEntreno?> GetByIdAsync(int id);
        Task<List<PreEntreno>> GetAllAsync(QueryParamsPreEntreno filtros);
        Task UpdateAsync(PreEntreno preEntreno);
        Task DeleteAsync(int id);
    }
}