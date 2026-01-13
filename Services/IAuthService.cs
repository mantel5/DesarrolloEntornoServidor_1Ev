using SuplementosAPI.DTOs;
using System.Security.Claims; // <--- Necesario para ClaimsPrincipal

namespace SuplementosAPI.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        
        // ¡NUEVO! El método del profe
        bool HasAccessToResource(int requestedUserId, ClaimsPrincipal user);
    }
}