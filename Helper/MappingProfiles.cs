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
            CreateMap<UserDto, User>();

            CreateMap<Groups, GroupsDto>();
            CreateMap<GroupsDto, Groups>();

            CreateMap<UserInGroup, UserInGroupDto>();
            CreateMap<UserInGroupDto, UserInGroup>();
            CreateMap<UserInGroup, UserDto>();
            CreateMap<UserInGroup, GroupsDto>();
        }
    }
}
