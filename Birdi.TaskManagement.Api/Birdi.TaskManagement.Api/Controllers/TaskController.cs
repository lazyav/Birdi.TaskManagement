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
        public readonly IUserService _userService;
        public readonly HttpContext _context;
        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
            _context = HttpContext;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ResponseObject> Add([FromBody] AddTask task)
        {
            Guid userId = _userService.GetUserId(HttpContext.User);
            await _taskService.Add(task, userId);
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

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Delete(string id)
        {
            await _taskService.Delete(new Guid(id));
            return ResponseObject.Create(System.Net.HttpStatusCode.OK);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Get(string id)
        {
            var task = await _taskService.Task(new Guid(id));
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: task);
        }


        [HttpGet("taskstatuses")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> Get()
        {
            var statues = await _taskService.GetTaskStatuses();
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: statues);
        }

        [HttpGet("all")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseObject> GetAll()
        {
            Guid userId = _userService.GetUserId(HttpContext.User);
            var tasks = await _taskService.Tasks(userId);
            return ResponseObject.Create(System.Net.HttpStatusCode.OK, _data: tasks);
        }
    }
}
