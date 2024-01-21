using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamAppAPI.Controllers
{
    // Controller für die Benutzerverwaltung
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Konstruktor zur Initialisierung des UserController
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Alle Benutzer abrufen
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            // Benutzerdaten abrufen und zu Dtos mappen
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            return Ok(users);
        }

        // Benutzer anhand der ID abrufen
        [HttpGet("id/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int userId)
        {
            // Überprüfen, ob der Benutzer anhand der ID existiert
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            // Benutzerdaten abrufen und zu Dto mappen
            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(userId));
            return Ok(user);
        }

        // Benutzer anhand der E-Mail-Adresse abrufen
        [HttpGet("mail/{userEmail}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByMail(string userEmail)
        {
            // Überprüfen, ob der Benutzer anhand der E-Mail-Adresse existiert
            if (!_userRepository.UserExistsByMail(userEmail))
                return NotFound();

            // Benutzerdaten abrufen und zu Dto mappen
            var user = _mapper.Map<UserDto>(_userRepository.GetUserByMail(userEmail));
            return Ok(user);
        }
    }
}