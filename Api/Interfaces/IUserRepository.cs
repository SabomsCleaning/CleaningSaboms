using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;

namespace CleaningSaboms.Interfaces
{
    public interface IUserRepository
    {
        Task<(IdentityResult Result, ApplicationUser User)> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string email);
        Task<bool> DeleteUserAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> UserExistsAsync(string email);
        Task<bool> AddUserToRoleAsync(string userId, string roleName);
        Task<IList<string>> GetRolesByIdAsync(string userId);
    }
}
