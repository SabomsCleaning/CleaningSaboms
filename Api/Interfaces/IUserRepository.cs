using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;

namespace CleaningSaboms.Interfaces
{
    public interface IUserRepository
    {
        Task<(IdentityResult Result, ApplicationUser User)> CreateUserAsync(ApplicationUser user, string password);
        Task UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> UserExistsAsync(string email);
        Task<IList<string>> GetUserRolesAsync(Guid userId);
        Task<bool> AddUserToRoleAsync(string userId, string roleName);
        Task<IList<string>> GetRolesByIdAsync(string userId);
    }
}
