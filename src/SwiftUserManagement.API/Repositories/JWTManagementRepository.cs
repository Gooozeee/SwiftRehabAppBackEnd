using Microsoft.IdentityModel.Tokens;
using SwiftUserManagement.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwiftUserManagement.API.Repositories
{
    // Concrete class for authenticating users using JWT
    public class JWTManagementRepository : IJWTManagementRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<JWTManagementRepository> _logger;

        public JWTManagementRepository(IConfiguration configuration, IUserRepository userRepository, ILogger<JWTManagementRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        // Code to see if a user matches, 
        public Tokens Authenticate(User user)
        {
            var foundUserFromDb = _userRepository.GetUserByEmail(user.Email);

           
           if(foundUserFromDb == null)
            {
                return new Tokens { Token = "Unauthorized" };
            }

            // If the user password and username don't match
            if (foundUserFromDb.Result.Email == user.Email && foundUserFromDb.Result.Password == user.Password)
            {
                // Generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
             new Claim(ClaimTypes.Name, user.UserName)
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                _logger.LogInformation("Token created for user");

                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }

            return new Tokens { Token = "Unauthorized" };

        }
    }
}
