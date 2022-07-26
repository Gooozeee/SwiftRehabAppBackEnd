using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftUserManagement.API.Entities;
using SwiftUserManagement.API.Repositories;
using System.Net;

namespace SwiftUserManagement.API.Controllers
{
    // User management controller
    [Route("api/[controller]")]
    [ApiController]
    public class SwiftUserManagementController : ControllerBase
    {

        // Dependency injection for the user repository to bring in business logic
        private readonly IUserRepository _userRepository;
        private readonly IJWTManagementRepository _jwtMangementRepository;
        private readonly IRabbitMQRepository _rabbitMQRepository;

        public SwiftUserManagementController(IUserRepository userRepository, IJWTManagementRepository jwtMangementRepository, IRabbitMQRepository rabbitMQRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtMangementRepository = jwtMangementRepository ?? throw new ArgumentNullException(nameof(jwtMangementRepository));
            _rabbitMQRepository = rabbitMQRepository ?? throw new ArgumentNullException(nameof(rabbitMQRepository));
        }



        // Creating a user in the database
        [HttpPost("createUser", Name = "CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> createUser([FromBody] User user)
        {
            // Validating the user
            if (string.IsNullOrEmpty(user.UserName) ^ string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "User is invalid" });
            }

            // Creating the user
            var result = await _userRepository.CreateUser(user);

            // Checking if the user exists or not
            if (result == false)
            {
                return BadRequest(new { Message = "User already exists" });
            }
            return CreatedAtRoute("CreateUser", new { userName = user.Password }, user);
        }

        // Retreiving a user from the database
        [HttpGet("{userName}", Name = "GetUser")]
        [Authorize]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> getUser(string userName)
        {
            var user = await _userRepository.GetUser(userName);

            if (user == null) return BadRequest(new { Message = "User not found" });

            return Ok(user);
        }

        // Letting the client know it is connected to the server
        [HttpGet("pingServer", Name = "Ping")]
        public string pingServer()
        {
            return "You are connected to the server";
        }

        // Authenticating a user and returning a JWT token
        [AllowAnonymous]
        [HttpPost("authenticate", Name = "Authenticate")]
        [ProducesResponseType(typeof(Tokens), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public ActionResult<Tokens> Authenticate(User user)
        {
            var token = _jwtMangementRepository.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        // Emitting the game results for analysis by the python file
        [HttpPost("analyseGameScore", Name = "AnalyseGameScore")]
        public ActionResult<bool> AnalyseGameResults([FromBody] GameResults gameResults)
        {
            var result = _rabbitMQRepository.EmitGameAnalysis(gameResults);
            if (!result) return BadRequest(new { Message = "User not found" });
            return Ok(result);
        }
    }
}
