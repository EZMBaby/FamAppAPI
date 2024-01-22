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
        #region Initialisierung
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Konstruktor zur Initialisierung des UserController
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region GET-Methoden


        // Alle Benutzer abrufen
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers() 
            => !ModelState.IsValid
            ? BadRequest(ModelState)
            : Ok(_mapper.Map<List<UserDto>>(_userRepository.GetUsers()));

        // Benutzer anhand der ID abrufen
        [HttpGet("id/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int userId) 
            => (!_userRepository.UserExistsById(userId)) 
            ? NotFound() 
            : Ok(_mapper.Map<UserDto>(_userRepository.GetUserById(userId)));

        // Benutzer anhand der E-Mail-Adresse abrufen
        [HttpGet("mail/{userEmail}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByMail(string userEmail)
            => (!_userRepository.UserExistsByMail(userEmail))
            ? NotFound()
            : Ok(_mapper.Map<UserDto>(_userRepository.GetUserByMail(userEmail)));

        #endregion

        #region POST-Methoden

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreation)
        {
            // Check ob userCreation leer ist
            // Wenn ja -> BadRequest
            if (userCreation == null)
                return BadRequest(ModelState);

            // Sucht nach einer Benutzer mit dieser E-Mail
            var user = _userRepository.GetUsers()
                .Where(u => u.email.Trim().ToUpper() == userCreation.email.TrimEnd().ToUpper())
                .FirstOrDefault();

            // Wenn eine Benutzer mit dieser E-Mail existiert -> BadRequest
            if (user != null)
            {
                ModelState.AddModelError("", "User with this E-Mail already exists");
                return StatusCode(422, ModelState);
            }

            // Check ob userCreation valide ist
            // Wenn nein -> BadRequest
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Erstellt eine neue Benutzer
            var userMap = _mapper.Map<User>(userCreation);

            if (!_userRepository.CreateUser(userMap))
            {
                // Wenn der Benutzer nicht erstellt werden konnte -> ErrorCode
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created user");
        }

        #endregion

        #region PUT-Methoden

        [HttpPut("update/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userUpdate)
        {
            // Check ob userUpdate leer ist
            // Wenn ja -> BadRequest
            if (userUpdate == null)
                return BadRequest(ModelState);

            // Check ob die gegebene ID mit der ID von userUpdate übereinstimmt
            // Wenn nicht -> BadRequest
            if (userId != userUpdate.id)
                return BadRequest(ModelState);

            // Check ob die Benutzer existiert
            // Wenn nicht -> NotFound
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            // Check ob userUpdate valide ist
            // Wenn nicht -> BadRequest
            if (!ModelState.IsValid)
                return BadRequest();

            // Update
            var userMap = _mapper.Map<User>(userUpdate);

            if (!_userRepository.UpdateUser(userMap))
            {
                // Wenn der Benutzer nicht aktualisiert werden konnte -> ErrorCode
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion
    }
}