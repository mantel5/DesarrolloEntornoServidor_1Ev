using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IOmega3Service
    {
        Task<Omega3> CreateAsync(Omega3CreateDto dto);
        
        Task<List<Omega3>> GetAllAsync(QueryParamsOmega3 filtros);
        
        Task<Omega3?> GetByIdAsync(int id);
        
        Task DeleteAsync(int id);
    }
}