using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(ApplicationUser user, string password);
        Task UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<ApplicationUser?> GetUserByIdAsync(Guid userId);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> UserExistsAsync(string email);
        Task<IList<string>> GetUserRolesAsync(Guid userId);
        Task AddUserToRoleAsync(Guid userId, string roleName);
    }
}
