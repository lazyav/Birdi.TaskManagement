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
            ResponseObject responseObject = null;
            //check if user already exists;
            UserDto userDto = await _userService.Get(addUser.UserName);
            if (userDto == null)
            {
                responseObject = ResponseObject.Create(System.Net.HttpStatusCode.OK);
                await _userService.Register(addUser);
            }
            else
            {
                responseObject = ResponseObject.Create(System.Net.HttpStatusCode.OK, _isSuccess: false, _error: "User already exists");
            }
            return CreatedAtAction(nameof(Register), responseObject);
        }


        [HttpPost]
        [Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            ResponseObject responseObject = await _userService.AuthenticateUser(loginUser);
            return Ok(responseObject);
        }
    }
}
