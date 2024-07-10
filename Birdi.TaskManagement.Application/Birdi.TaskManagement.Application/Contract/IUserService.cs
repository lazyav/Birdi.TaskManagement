using Birdi.TaskManagement.Application.Models;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface IUserService
    {
        Task Register(AddUser addUser);
    }
}
