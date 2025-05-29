using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task AddUserToRoleAsync(Guid userId, string roleName)
        {
            throw new NotImplementedException();
        }

        private async Task<UserDto> BuildUserDtoAsync(ApplicationUser user)
        {
            var roles = await _userRepository.GetRolesByIdAsync(user.Id);

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.UserFirstName,
                LastName = user.UserLastName,
                PhoneNumber = user.UserPhone,
                Roles = roles.ToList(),
            };
        }

        public async Task<ServiceResult<UserDto>> CreateUserAsync(RegisterUserDto dto)
        {
            var user = UserFactory.FromDtoToApplicationUser(dto);

            var (result, createdUser) = await _userRepository.CreateUserAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return ServiceResult<UserDto>.Fail("Användaren kunde inte skapas", ErrorType.NotFound);
            }

            var roleExist = await _userRepository.AddUserToRoleAsync(createdUser.Id, dto.Role);
            if (!roleExist)
                return ServiceResult<UserDto>.Fail("Kunde inte skapa en roll till användaren", ErrorType.NotFound);
            var userDto = UserFactory.FromApplicationUserToDto(user);
            return ServiceResult<UserDto>.Ok(userDto, "Användaren är skapad");
        }

        //TODO: Denna skall vara en serviceResult
        //TODO: skall vara email
        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByEmailAsync(id);
            if (user == null) {  return false; }

            return await _userRepository.DeleteUserAsync(user);

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var dto = await BuildUserDtoAsync(user);
                usersDto.Add(dto);
            }
            return usersDto;
        }

        public async Task<ServiceResult<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
                return ServiceResult<UserDto>.Fail("Användaren kunde inte hittas", ErrorType.NotFound);

            var userDto = await BuildUserDtoAsync(user);
            return ServiceResult<UserDto>.Ok(userDto, "Användaren hittad");
        }

        public Task<ServiceResult<ApplicationUser?>> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<UserDto>> UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
