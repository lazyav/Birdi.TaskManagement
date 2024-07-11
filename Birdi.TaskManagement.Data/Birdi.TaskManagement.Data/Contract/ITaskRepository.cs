using Birdi.TaskManagement.Core.Entity;

namespace Birdi.TaskManagement.Data.Contract
{
    public interface ITaskRepository
    {
        Task Delete(Guid id);
        Task<IEnumerable<UserTask>> Tasks(Guid userId);
        Task<UserTask> Task(Guid id);
        Task Add(UserTask task);
        Task Edit(UserTask task);
    }
}
