using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Birdi.TaskManagement.Api.Controllers
{
    [Authorize]
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
        public async Task<ResponseObject> Delete([FromBody] string taskId)
        {
            await _taskService.Delete(new Guid(taskId));
            return ResponseObject.Create(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Get([FromBody] string taskId)
        {
            var task = await _taskService.Task(new Guid(taskId));
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: task);
        }

        [HttpGet("all/{id}")]
        //[Route("all")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> GetAll(string id)
        {
            var tasks = await _taskService.Tasks(new Guid(id));
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: tasks);
        }
    }
}
