using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftUserManagement.Entities;
using SwiftUserManagement.Repositories;
using System.Net;

namespace SwiftUserManagement.Controllers
{
    // User management controller
    [Route("api/[controller]")]
    [ApiController]
    public class SwiftUserManagementController : ControllerBase
    {
        // Dependency injection for the user repository to bring in business logic
        private readonly IUserRepository _userRepository;

        public SwiftUserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // Creating a user in the database
        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> createUser([FromBody] User user)
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
            return Ok(result);
        }
    }
}
