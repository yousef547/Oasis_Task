using AutoMapper;
using oasis.Entities;
using oasis.DTOs;
using System.Linq;
using oasis.Extensions;

namespace oasis.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ToDoUsers,ToDoDto>().ReverseMap();
            CreateMap<ToDoUsers, UpdateToDoDto>().ReverseMap();
        }
    }
}
