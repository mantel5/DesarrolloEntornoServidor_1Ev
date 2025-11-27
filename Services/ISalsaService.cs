using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface ISalsaService
    {
        // Crear (Recibe DTO, devuelve Modelo validado)
        Task<Salsa> CreateAsync(SalsaCreateDto dto);
        
        // Leer todos (Recibe filtros de Salsas y de Macros)
        Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros);
        
        // Leer uno
        Task<Salsa?> GetByIdAsync(int id);
        
        // Borrar
        Task DeleteAsync(int id);
    }
}