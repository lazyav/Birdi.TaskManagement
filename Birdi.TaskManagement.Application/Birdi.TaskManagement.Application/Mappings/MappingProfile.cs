using AutoMapper;
using Birdi.TaskManagement.Application.Models;
using Birdi.TaskManagement.Core.Entity;

namespace Birdi.TaskManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUser, User>();
        }

    }
}
