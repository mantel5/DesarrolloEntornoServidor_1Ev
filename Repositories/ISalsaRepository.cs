using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface ISalsaRepository
    {
        // CRUD Básico
        Task AddAsync(Salsa salsa);
        Task<Salsa?> GetByIdAsync(int id);
        
        // Búsqueda con filtros de Comida (Macros) + Salsa (Picante, Zero)
        Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros);
        
        Task UpdateAsync(Salsa salsa);
        Task DeleteAsync(int id);
    }
}