using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FamAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInGroupsController : ControllerBase
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IGroupsRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersInGroupsController(
            IUserInGroupRepository userInGroupRepository, 
            IGroupsRepository groupRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userInGroupRepository = userInGroupRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region GET-Methoden

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserInGroup>))]
        public IActionResult GetAllUsersInGroups() 
            => Ok(_mapper.Map<List<UserInGroupDto>>(_userInGroupRepository.GetAllUsersInGroups()));

        [HttpGet("GetGroupsOfUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Groups>))]
        public IActionResult GetGroupsOfUser(int userId) 
            => !_userRepository.UserExistsById(userId) 
            ? NotFound() 
            : Ok(_mapper.Map<List<GroupsDto>>(_userInGroupRepository.GetGroupsOfUser(userId)));

        [HttpGet("GetUsersInGroup/{groupId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsersInGroup(int groupId) 
            => !_groupRepository.GroupExistsById(groupId) 
            ? NotFound() 
            : Ok(_mapper.Map<List<UserDto>>(_userInGroupRepository.GetUsersInGroup(groupId)));

        #endregion

        #region POST-Methoden

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserInGorup([FromBody] UserInGroupDto userInGroupCreation)
        {
            if (userInGroupCreation == null)
                return BadRequest(ModelState);

            var group = _userInGroupRepository.GetAllUsersInGroups()
                .Where(ug => ug.UserId == userInGroupCreation.UserId 
                        && ug.GroupId == userInGroupCreation.GroupId)
                .FirstOrDefault();

            if (group != null)
            {
                ModelState.AddModelError("", "User is already in this Group");
                return StatusCode(422, ModelState);
            }

            var groupMap = _mapper.Map<UserInGroup>(userInGroupCreation);

            if (!_userInGroupRepository.CreateUserInGroup(groupMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created group");
        }

        #endregion

        #region DELETE-Methoden

        [HttpDelete("{userId}/{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserFromGroup(int userId, int groupId)
        {
            // Check ob der Benutzer in der Gruppe existiert
            // Wenn nicht -> NotFound
            if (!_userInGroupRepository.CheckIfUserIsInGroup(userId, groupId))
                return NotFound();

            // Sucht die Benutzer-Gruppen Relation in der Datenbank
            var userToDeleteFromGroup = _userInGroupRepository
                .GetUserInGroup(userId, groupId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userInGroupRepository.DeleteUserInGroup(userToDeleteFromGroup))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion
    }
}
