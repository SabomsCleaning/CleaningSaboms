using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using Microsoft.AspNetCore.Identity;

namespace CleaningSaboms.Services
{
    public class UserService(IUserRepository userRepository, IAuditLogger auditLogger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuditLogger _auditLogger = auditLogger;

        private async Task<UserDto> BuildUserDtoAsync(ApplicationUser user)
        {
            var roles = await _userRepository.GetRolesByIdAsync(user.Id);

            return new UserDto
            {
                Email = user.Email!,
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
                await _auditLogger.LogAsync(
                    action: "CreateUserFailed",
                    //TODO: Denna skall ändras när inlogg är på plats
                    performedBy: "[System/Admin]",
                    target: dto.Email,
                    details: "User creation failed via UserManager"
                    );
                return ServiceResult<UserDto>.Fail("Användaren kunde inte skapas", ErrorType.NotFound);
            }

            var roleExist = await _userRepository.AddUserToRoleAsync(createdUser.Id, dto.Role);
            if (!roleExist)
            {
                await _auditLogger.LogAsync(
                action: "AddUserToRoleFailed",
                performedBy: "[System/Admin]",
                target: dto.Email,
                details: $"Role '{dto.Role}' could not be assigned"
                );

                return ServiceResult<UserDto>.Fail("Kunde inte skapa en roll till användaren", ErrorType.NotFound);
            }

            await _auditLogger.LogAsync(
        action: "CreateUserSuccess",
        performedBy: "[System/Admin]", // TODO: Hämta från inloggad användare senare
        target: dto.Email,
        details: $"User created and assigned role '{dto.Role}'"
    );

            var userDto = UserFactory.FromApplicationUserToDto(user);
            return ServiceResult<UserDto>.Ok(userDto, "Användaren är skapad");
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                await _auditLogger.LogAsync(
                    action: "DeleteUserFailed",
                    //TODO: Denna skall ändras när inlogg är på plats
                    performedBy: "[System/Admin]",
                    target: email,
                    details: "User not found"
                    );
                return ServiceResult<bool>.Fail("Användaren kunde inte hittas", ErrorType.NotFound);
            }

            var success = await _userRepository.DeleteUserAsync(user);
            await _auditLogger.LogAsync(
            action: success ? "DeleteUserSuccess" : "DeleteUserFailed",
            //TODO: Denna skall ändras när inlogg är på plats
            performedBy: "[system/admin]", // byt ut mot riktig användare
            target: email,
            details: success ? "User successfully deleted" : "Failed to delete user"
            );

            if (!success)
            {

                return ServiceResult<bool>.Fail("Användaren kunde inte tas bort");
            }
            return ServiceResult<bool>.Ok(true, "Användaren togs bort");
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

        public async Task<ServiceResult<UpdateUserDto>> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return ServiceResult<UpdateUserDto>.Fail("Användaren kunde inte hittas", ErrorType.NotFound);

            var userDto = new UpdateUserDto {
                Id = user.Id,
                FirstName = user.UserFirstName,
                LastName = user.UserLastName,
                PhoneNumber = user.PhoneNumber,
            };

            return ServiceResult<UpdateUserDto>.Ok(userDto, "Användaren hittad");
        }

        public async Task<ServiceResult> UpdateUserAsync(UpdateUserDto user)
        {
            var userExist = await _userRepository.GetUserByIdAsync(user.Id);
            if (userExist == null)
                return ServiceResult.Fail("Användaren kunde inte hittas", ErrorType.NotFound);

            userExist.UserFirstName = user.FirstName!;
            userExist.UserLastName = user.LastName!;
            userExist.UserPhone = user.PhoneNumber!;

            var result = await _userRepository.UpdateUserAsync(userExist, user.Email!);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResult.Fail($"Användaren kunde inte uppdateras: {errors}", ErrorType.Conflict);
            }

            await _auditLogger.LogAsync(
                    action: "UpdateUserPerformed",
                    //TODO: Denna skall ändras när inlogg är på plats
                    performedBy: "[System/Admin]",
                    target: user.Email!,
                    details: "Update User"
                    );

            return ServiceResult.Ok("Användaren är uppdaterad");
        }

    }
}
