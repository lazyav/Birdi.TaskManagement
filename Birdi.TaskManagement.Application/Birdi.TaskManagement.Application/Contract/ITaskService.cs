using Birdi.TaskManagement.Application.Models;

namespace Birdi.TaskManagement.Application.Contract
{
    public interface ITaskService
    {
        Task Delete(Guid id);
        Task<IEnumerable<TaskDto>> Tasks(Guid userId);
        Task<TaskDto> Task(Guid id);
        Task Add(AddTask task, Guid userId);
        Task Edit(EditTask task);
        Task<IEnumerable<TaskStatusDto>> GetTaskStatuses();
    }
}
