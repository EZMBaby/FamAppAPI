using AutoMapper;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace FamAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInGroupsController : ControllerBase
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IMapper _mapper;

        public UsersInGroupsController(IUserInGroupRepository userInGroupRepository, IMapper mapper)
        {
            _userInGroupRepository = userInGroupRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserInGroup>))]
        public IActionResult GetAllUsersInGroups()
        {
            var usersInGroups = _userInGroupRepository.GetAllUsersInGroups();
            return Ok(usersInGroups);
        }
    }
}
