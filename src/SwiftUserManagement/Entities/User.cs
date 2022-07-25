using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwiftUserManagement.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Last_Login_At { get; set; }
        public DateTime Current_Login_At { get; set; }
        public int Login_Count { get; set; }
        public string Role { get; set; }
    }
}
