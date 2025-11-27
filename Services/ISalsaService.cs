using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface ISalsaService
    {
        Task<Salsa> CreateAsync(SalsaCreateDto dto);
        Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros);
        Task<Salsa?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}