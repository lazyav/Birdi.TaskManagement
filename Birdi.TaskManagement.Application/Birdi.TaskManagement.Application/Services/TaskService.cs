using AutoMapper;
using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Birdi.TaskManagement.Core.Entity;
using Birdi.TaskManagement.Data.Contract;

namespace Birdi.TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        public readonly ITaskRepository _taskRepository;
        public readonly IMapper _mapping;
        public TaskService(ITaskRepository taskRepository, IMapper mapping)
        {
            _taskRepository = taskRepository;
            _mapping = mapping;
        }
        public async Task Add(AddTask task)
        {
            UserTask userTask = _mapping.Map<UserTask>(task);
            userTask.Id = Guid.NewGuid();
            await _taskRepository.Add(userTask);
        }

        public async Task Delete(Guid id)
        {
            await _taskRepository.Delete(id);
        }

        public async Task Edit(EditTask task)
        {
            UserTask userTask = _mapping.Map<UserTask>(task);
            userTask.UserId = new Guid("9F5205DB-D501-4EE2-8DD0-D02E2A2329FA");
            await _taskRepository.Edit(userTask);
        }

        public async Task<TaskDto> Task(Guid id)
        {
            var task = await _taskRepository.Task(id);
            TaskDto taskDto = _mapping.Map<TaskDto>(task);
            return taskDto;
        }

        public async Task<IEnumerable<TaskDto>> Tasks(Guid userId)
        {
            var task = await _taskRepository.Tasks(userId);
            IEnumerable<TaskDto> taskList = _mapping.Map<IEnumerable<TaskDto>>(task);
            return taskList;
        }
    }
}
