using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario usuario);
        
        Task<Usuario?> GetByEmailAsync(string email);
        
        Task<bool> ExistsEmailAsync(string email);

        Task<Usuario?> GetByIdAsync(int id);
        
        Task<List<Usuario>> GetAllAsync(QueryParamsUsuario filtros);
    }
}