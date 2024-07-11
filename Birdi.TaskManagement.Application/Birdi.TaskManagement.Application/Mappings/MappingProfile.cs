using AutoMapper;
using Birdi.TaskManagement.Application.Models;
using Birdi.TaskManagement.Core.Entity;

namespace Birdi.TaskManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<AddUser, User>();
            CreateMap<AddTask, UserTask>();
            CreateMap<EditTask, UserTask>();

            CreateMap<UserTask, TaskDto>();
            CreateMap<TaskDto, UserTask>();
            CreateMap<User, UserDto>();
        }
    }
}
