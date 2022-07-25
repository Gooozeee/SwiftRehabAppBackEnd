using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftUserManagement.Entities;

namespace SwiftUserManagement.Repositories
{

    public interface IUserRepository 
    {
        Task<User> GetUser(string userName);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
    }
}
