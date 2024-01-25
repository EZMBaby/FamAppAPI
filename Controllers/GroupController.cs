using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FamAppAPI.Controllers
{
    // Der Controller zur Bearbeitung von API-Anfragen im Zusammenhang mit Gruppen
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        #region Initialisierung

        private readonly IGroupsRepository _groupRepository;
        private readonly IMapper _mapper;

        // Konstruktor zur Initialisierung des GroupController
        public GroupController(IGroupsRepository groupsRepository, IMapper mapper)
        {
            _groupRepository = groupsRepository;
            _mapper = mapper;
        }

        #endregion

        #region GET-Methoden

        // Alle Gruppen abrufen
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        public IActionResult GetGroups()
            => !ModelState.IsValid 
            ? BadRequest(ModelState) 
            : Ok(_mapper.Map<List<GroupsDto>>(_groupRepository.GetGroups()));

        // Eine Gruppe anhand der ID abrufen
        [HttpGet("id/{groupId}")]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(400)]
        public IActionResult GetGroupById(int groupId)
            => !_groupRepository.GroupExistsById(groupId)
            ? NotFound()
            : Ok(_mapper.Map<GroupsDto>(_groupRepository.GetGroupById(groupId)));

        #endregion

        #region POST-Methoden

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGroup([FromBody] GroupsDto groupCreation)
        {
            // Check ob groupCreation leer ist -> BadRequest
            if (groupCreation == null)
                return BadRequest(ModelState);

            // Sucht ob eine Gruppe mit dieser ID existiert
            var group = _groupRepository.GetGroups()
                .Where(g => g.id == groupCreation.id)
                .FirstOrDefault();

            // Wenn eine Gruppe mit dieser ID existiert -> BadRequest
            if (group != null)
            {
                ModelState.AddModelError("", "Group with this ID already exists");
                return StatusCode(422, ModelState);
            }

            // Check ob groupCreation valide ist -> BadRequest
            var groupMap = _mapper.Map<Groups>(groupCreation);

            if (!_groupRepository.CreateGroup(groupMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(groupMap);
        }

        #endregion

        #region PUT-Methoden

        [HttpPut("update/{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int groupId, [FromBody] GroupsDto groupUpdate)
        {
            // Check ob groupUpdate leer ist
            // Wenn ja -> BadRequest
            if (groupUpdate == null)
                return BadRequest(ModelState);

            // Check ob die gegebene ID mit der ID von groupUpdate übereinstimmt
            // Wenn nicht -> BadRequest
            if (groupId != groupUpdate.id)
                return BadRequest(ModelState);

            // Check ob die Gruppe existiert
            // Wenn nicht -> NotFound
            if (!_groupRepository.GroupExistsById(groupId))
                return NotFound();

            // Check ob groupUpdate valide ist
            // Wenn nicht -> BadRequest
            if (!ModelState.IsValid)
                return BadRequest();

            // Update
            var groupMap = _mapper.Map<Groups>(groupUpdate);

            if (!_groupRepository.UpdateGroup(groupMap))
            {
                // Fehlermeldung ausgeben, falls das Update fehlschlägt
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region DELETE-Methoden

        [HttpDelete("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int groupId)
        {
            // Check ob der Benutzer existiert
            // Wenn nicht -> NotFound
            if (!_groupRepository.GroupExistsById(groupId))
                return NotFound();

            // Sucht den Benutzer in der Datenbank
            var groupToDelete = _groupRepository.GetGroupById(groupId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_groupRepository.DeleteGroup(groupToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion
    }
}