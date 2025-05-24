using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task CreateUserAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public Task<bool> UserExistsAsync(string email)
        {
            throw new NotImplementedException();
        }
        public Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task AddUserToRoleAsync(Guid userId, string roleName)
        {
            throw new NotImplementedException();
        }
    }
    
}

