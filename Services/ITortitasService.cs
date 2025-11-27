using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface ITortitasService
    {
        Task<Tortitas> CreateAsync(TortitasCreateDto dto);
        Task<List<Tortitas>> GetAllAsync(QueryParamsTortitas filtros);
        Task<Tortitas?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}