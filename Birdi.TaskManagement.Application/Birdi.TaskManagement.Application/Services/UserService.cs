using AutoMapper;
using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Application.Models;
using Birdi.TaskManagement.Core.Entity;
using Birdi.TaskManagement.Data.Contract;

namespace Birdi.TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Register(AddUser addUser)
        {
            User user = _mapper.Map<User>(addUser);
            user.Id = Guid.NewGuid();
            await _userRepository.Add(user);
        }
    }
}
