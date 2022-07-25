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

        public SwiftUserManagementController(IUserRepository userRepository, IJWTManagementRepository jwtMangementRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtMangementRepository = jwtMangementRepository ?? throw new ArgumentNullException(nameof(jwtMangementRepository));
        }

        // Creating a user in the database
        [HttpPost(Name = "CreateUser")]
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
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> getUser(string userName)
        {
            var user = await _userRepository.GetUser(userName);

            if (user == null) return BadRequest(new { Message = "User not found" });

            return Ok(user);
        }

        // Authenticating a user and returning a JWT token
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(User user)
        {
            var token = _jwtMangementRepository.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
