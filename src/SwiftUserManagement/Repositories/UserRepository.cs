using SwiftUserManagement.Entities;

namespace SwiftUserManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
