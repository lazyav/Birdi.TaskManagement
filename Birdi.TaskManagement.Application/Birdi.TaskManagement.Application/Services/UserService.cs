using AutoMapper;
using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Birdi.TaskManagement.Core.Entity;
using Birdi.TaskManagement.Core.Util;
using Birdi.TaskManagement.Data.Contract;

namespace Birdi.TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IAuthService _authService;
        public readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IAuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task Register(AddUser addUser)
        {
            User user = _mapper.Map<User>(addUser);
            user.Password = Password.HashPassword(addUser.Password);
            user.Id = Guid.NewGuid();
            await _userRepository.Add(user);
        }
        public async Task<UserDto> Get(string userName)
        {
            User user = await _userRepository.Get(userName);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<ResponseObject> AuthenticateUser(LoginUser loginUser)
        {
            ResponseObject responseObject = null;
            User user = await _userRepository.Get(loginUser.UserName); //get the user details from database
            if (user == null)
            {
                responseObject = ResponseObject.Create(System.Net.HttpStatusCode.OK, _isSuccess: false, _error: "User is not registered.");
            }
            else
            {
                if (Password.VerifyPassword(loginUser.Password, user.Password))
                {
                    string token = _authService.GenerateToken(user.Id);
                    responseObject = ResponseObject.Create(System.Net.HttpStatusCode.OK, _isSuccess: true, _data: new token() { accessToken = token });
                }
                else
                {
                    responseObject = ResponseObject.Create(System.Net.HttpStatusCode.OK, _isSuccess: false, _error: "Invalid username or password.");
                }
            }

            return responseObject;
        }
    }
}
