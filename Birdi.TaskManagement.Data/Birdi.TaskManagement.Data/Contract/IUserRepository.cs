using Birdi.TaskManagement.Core.Entity;

namespace Birdi.TaskManagement.Data.Contract
{
    public interface IUserRepository
    {
        Task Add(User user);
    }
}
