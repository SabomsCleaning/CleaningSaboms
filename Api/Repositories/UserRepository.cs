using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<(IdentityResult Result, ApplicationUser User)> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return (result, user);
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
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email); 
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public Task<bool> UserExistsAsync(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
                return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetRolesByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            var role = await _userManager.GetRolesAsync(user);
            return role;
        }
    }

}

