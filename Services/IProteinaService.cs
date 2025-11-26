using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IProteinaService
    {
        Task<Proteina> CreateAsync(ProteinaCreateDto dto);
        Task<List<Proteina>> GetAllAsync(QueryParamsProteina filtros);
        Task<Proteina?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}