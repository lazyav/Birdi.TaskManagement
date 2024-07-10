using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Birdi.TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] AddUser addUser)
        {
            await _userService.Register(addUser);
            ResponseObject responseObject = ResponseObject.Create(System.Net.HttpStatusCode.Created);
            return CreatedAtAction(nameof(Register), responseObject);
        }
    }
}
