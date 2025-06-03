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
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string? newEmail = null)
        {

            //TODO: Det är här vi fortsätter i morgon, Den uppdaterar inte userName utan den ligger kvar som den gamla men nu är jag trött

            // Om e-post ändrats – använd rätt metod
            if (!string.IsNullOrWhiteSpace(newEmail) && !string.Equals(user.Email, newEmail, StringComparison.OrdinalIgnoreCase))
            {
                var emailResult = await _userManager.SetEmailAsync(user, newEmail);
                if (!emailResult.Succeeded)
                    return emailResult;

                var userNameResult = await _userManager.SetUserNameAsync(user, newEmail);
                if (!userNameResult.Succeeded)
                    return userNameResult;
            }
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
           return await _userManager.FindByIdAsync(userId);
        }
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email); 
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return false;
            }
            return true;
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

        public Task<bool> DeleteUserAsync(string email)
        {
            throw new NotImplementedException();
        }
    }

}

