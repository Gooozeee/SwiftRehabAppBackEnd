using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftUserManagement.Entities;
using SwiftUserManagement.Repositories;
using System.Net;

namespace SwiftUserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwiftUserManagementController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public SwiftUserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> createUser()
        {

        }
    }
}
