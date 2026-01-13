using SuplementosAPI.Dtos;
using SuplementosAPI.DTOs;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IOpinionService
    {
        // Fíjate que devuelve OpinionDto
        Task<IEnumerable<OpinionDto>> GetAllAsync(OpinionQueryParams? queryParams = null);
        
        // Fíjate que recibe OpinionCreateDto
        Task AddAsync(OpinionCreateDto dto);
        
        Task DeleteAsync(int id);
    }
}