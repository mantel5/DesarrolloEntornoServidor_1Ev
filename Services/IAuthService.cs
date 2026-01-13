using SuplementosAPI.DTOs;

namespace SuplementosAPI.Services
{
    public interface IAuthService
    {
        // Devuelve el Token (string) si todo va bien, o null si falla
        Task<string> LoginAsync(LoginDto loginDto);
    }
}