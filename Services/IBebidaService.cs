using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IBebidaService
    {
        Task<Bebida> CreateAsync(BebidaCreateDto dto);
        Task<List<Bebida>> GetAllAsync(QueryParamsBebida filtros);
        Task<Bebida?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}