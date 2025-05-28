using CleaningSaboms.Dto;
using CleaningSaboms.Models;

namespace CleaningSaboms.Factory
{
    public class UserFactory
    {
        public static ApplicationUser FromDtoToApplicationUser(RegisterUserDto dto) {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                UserFirstName = dto.FirstName ?? string.Empty,
                UserLastName = dto.LastName ?? string.Empty,
                UserPhone = dto.PhoneNumber ?? string.Empty
            };
            return user;
        }

        public static UserDto FromApplicationUserToDto(ApplicationUser user)
        {
            return new UserDto
            {
                Email = user.Email,
                FirstName = user.UserFirstName,
                LastName = user.UserLastName,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
