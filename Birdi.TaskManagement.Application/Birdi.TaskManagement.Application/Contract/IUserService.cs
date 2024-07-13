using Birdi.TaskManagement.Application.Models;
using System.Security.Claims;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface IUserService
    {
        Task Register(AddUser addUser);

        Task<UserDto> Get(string userName);

        Task<ResponseObject> AuthenticateUser(LoginUser loginUser);

        Guid GetUserId(ClaimsPrincipal user);
    }
}
