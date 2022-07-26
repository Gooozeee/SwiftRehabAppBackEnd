using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftUserManagement.API.Entities;

namespace SwiftUserManagement.API.Repositories
{

    public interface IUserRepository 
    {
        Task<User> GetUser(string userName);
        Task<User> GetUserByEmail(string email);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
    }
}
