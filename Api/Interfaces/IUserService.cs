using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface IUserService
    {
        Task <ServiceResult<UserDto>>CreateUserAsync(RegisterUserDto dto);
        Task <ServiceResult<UserDto>>UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<ServiceResult<ApplicationUser?>> GetUserByIdAsync(Guid userId);
        Task<ServiceResult<UserDto>> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> UserExistsAsync(string email);
        Task<IList<string>> GetUserRolesAsync(Guid userId);
        Task AddUserToRoleAsync(Guid userId, string roleName);
    }
}
