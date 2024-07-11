using Birdi.TaskManagement.Application.Models;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface ITaskService
    {
        Task Delete(Guid id);
        Task<IEnumerable<TaskDto>> Tasks(Guid userId);
        Task<TaskDto> Task(Guid id);
        Task Add(AddTask task);
        Task Edit(EditTask task);
    }
}
