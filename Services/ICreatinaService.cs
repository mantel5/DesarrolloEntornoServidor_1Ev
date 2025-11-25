using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface ICreatinaService
    {
        Task<Creatina> CreateAsync(CreatinaCreateDto dto);
        Task<List<Creatina>> GetAllAsync(QueryParamsCreatina filtros);
        Task<Creatina?> GetByIdAsync(int id);
        Task UpdateAsync(int id, CreatinaCreateDto dto); // Opcional por ahora
        Task DeleteAsync(int id);
    }
}