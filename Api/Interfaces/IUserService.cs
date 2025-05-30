using CleaningSaboms.Dto;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface IUserService
    {
        Task <ServiceResult<UserDto>>CreateUserAsync(RegisterUserDto dto);
        Task <ServiceResult<UserDto>>UpdateUserAsync(ApplicationUser user);
        Task<ServiceResult<bool>> DeleteUserAsync(string id);
        Task<ServiceResult<UserDto>> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
