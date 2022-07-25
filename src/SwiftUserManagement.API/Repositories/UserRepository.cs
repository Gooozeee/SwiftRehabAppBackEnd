using Dapper;
using Npgsql;
using SwiftUserManagement.Entities;

namespace SwiftUserManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Dependency injection for the configuration to get the postgresql connection string
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Creating a new user and adding them into the database
        public async Task<bool> CreateUser(User user)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //connection.Open();

            var affected = await connection.ExecuteAsync
                ("INSERT INTO Users(Email, UserName, Password, Role) VALUES(@Email, @Username, @Password, @Role);",
                new { Email = user.Email, Username = user.UserName, Password = user.Password, Role = user.Role});

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public Task<User> GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
