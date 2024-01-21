using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Models;

namespace FamAppAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Groups, GroupsDto>();
        }
    }
}
