using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Services
{
    public interface IUsuarioService
    {
        // Recibe datos de registro -> Devuelve usuario 
        Task<UsuarioViewDto> RegisterAsync(UsuarioRegisterDto dto);

        // Recibe credenciales -> Devuelve usuario 
        Task<UsuarioViewDto?> LoginAsync(UsuarioLoginDto dto);

        // Busca por ID -> Devuelve usuario 
        Task<UsuarioViewDto?> GetByIdAsync(int id);

        // Devuelve lista de usuarios
        Task<List<UsuarioViewDto>> GetAllAsync(QueryParamsUsuario filtros);
    }
}