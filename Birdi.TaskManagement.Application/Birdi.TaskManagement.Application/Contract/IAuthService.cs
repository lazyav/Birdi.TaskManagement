using System.Security.Claims;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface IAuthService
    {
        string GenerateToken(Guid userId);
        ClaimsPrincipal ValidateToken(string token);
    }
}
