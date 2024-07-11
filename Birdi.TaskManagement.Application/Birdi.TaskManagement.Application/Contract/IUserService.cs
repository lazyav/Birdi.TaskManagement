using Birdi.TaskManagement.Application.Models;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface IUserService
    {
        Task Register(AddUser addUser);

        Task<UserDto> Get(string userName);

        Task<ResponseObject> AuthenticateUser(LoginUser loginUser);
    }
}
