using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IOpinionService
    {
        Task<Opinion> CreateAsync(OpinionCreateDto dto);
        Task<List<Opinion>> GetAllAsync(OpinionQueryParams queryParams);
        Task DeleteAsync(int id);
    }
}