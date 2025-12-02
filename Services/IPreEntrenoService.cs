using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IPreEntrenoService
    {
        Task<PreEntreno> CreateAsync(PreEntrenoCreateDto dto);
        Task<List<PreEntreno>> GetAllAsync(QueryParamsPreEntreno filtros);
        Task<PreEntreno?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(PreEntreno nuevaPreEntreno);
    }
}