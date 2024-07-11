using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Birdi.TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Route("add")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ResponseObject> Add([FromBody] AddTask task)
        {
            await _taskService.Add(task);
            return ResponseObject.Create(System.Net.HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("edit")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Edit([FromBody] EditTask task)
        {
            await _taskService.Edit(task);
            return ResponseObject.Create(System.Net.HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Delete(Guid taskId)
        {
            await _taskService.Delete(taskId);
            return ResponseObject.Create(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Get(Guid taskId)
        {
            var task = await _taskService.Task(taskId);
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: task);
        }

        [HttpGet]
        [Route("all")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> GetAll(Guid userId)
        {
            var tasks = await _taskService.Tasks(userId);
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: tasks);
        }
    }
}
