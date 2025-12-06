using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuplementosAPI.Services
{
    public interface IPacksService
    {
        Task<List<Packs>> GetAllAsync(QueryParamsPacks filtros);
        Task<Packs?> GetByIdAsync(int id);
        Task<Packs> CreateAsync(PacksCreateDto dto);
        Task UpdateAsync(int id, PacksCreateDto dto);
        Task DeleteAsync(int id);
    }
}