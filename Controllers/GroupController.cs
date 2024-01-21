using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FamAppAPI.Controllers
{
    // Der Controller zur Bearbeitung von API-Anfragen im Zusammenhang mit Gruppen
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupsRepository _groupRepository;
        private readonly IMapper _mapper;

        // Konstruktor zur Initialisierung des GroupController
        public GroupController(IGroupsRepository groupsRepository, IMapper mapper)
        {
            _groupRepository = groupsRepository;
            _mapper = mapper;
        }

        // Alle Gruppen abrufen
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        public IActionResult GetGroups()
        {
            // Mappen der Gruppen aus dem Repository zu DTOs mithilfe von AutoMapper
            var groups = _mapper.Map<List<GroupsDto>>(_groupRepository.GetGroups());
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(groups);
        }

        // Eine Gruppe anhand der ID abrufen
        [HttpGet("id/{groupId}")]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(400)]
        public IActionResult GetGroupById(int groupId)
        {
            // Überprüfen, ob die Gruppe anhand der angegebenen ID existiert
            if (!_groupRepository.GroupExistsById(groupId))
                return NotFound();

            // Mappen der Gruppe aus dem Repository zu DTOs mithilfe von AutoMapper
            var group = _mapper.Map<GroupsDto>(_groupRepository.GetGroupById(groupId));
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(group);
        }
    }
}