using Microsoft.AspNetCore.Identity;
using Veiculos.API.Models.Identity;

namespace Veiculos.API.Service
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<SignInResult> CheckUserPasswordAsync(User userUpdate, string senha);
        Task<User> AddUser(User model);
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string username);
        Task<User> UpdateUser(User model);
        Task<bool> DeleteUser(int id);
    }
}
