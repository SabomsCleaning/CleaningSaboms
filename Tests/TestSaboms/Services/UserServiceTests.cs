using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using CleaningSaboms.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningSaboms.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IAuditLogger> _auditLoggerMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _auditLoggerMock = new Mock<IAuditLogger>();
            _userService = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);
        }

        [Fact]
        public async Task DeleteUserAsync_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            _userRepoMock.Setup(repo => repo.GetUserByEmailAsync("test@example.com"))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userService.DeleteUserAsync("test@example.com");

            // Assert
            Assert.False(result.Success);
            Assert.Equal(ErrorType.NotFound, result.Error);
        }

        [Fact]
        public async Task DeleteUserAsync_DeleteFails_ReturnsFailure()
        {
            // Arrange
            var user = new ApplicationUser { Email = "test@example.com" };

            _userRepoMock.Setup(repo => repo.GetUserByEmailAsync("test@example.com"))
                .ReturnsAsync(user);

            _userRepoMock.Setup(repo => repo.DeleteUserAsync(user))
                .ReturnsAsync(false); // simulerar misslyckande

            // Act
            var result = await _userService.DeleteUserAsync("test@example.com");

            // Assert
            Assert.False(result.Success);
            Assert.Equal(ErrorType.UnexpectedError, result.Error);
            Assert.Equal("Användaren kunde inte tas bort", result.Message);
        }

        [Fact]
        public async Task DeleteUserAsync_Success_ReturnsOk()
        {
            // Arrange
            var user = new ApplicationUser { Email = "test@example.com" };

            _userRepoMock.Setup(repo => repo.GetUserByEmailAsync("test@example.com"))
                .ReturnsAsync(user);

            _userRepoMock.Setup(repo => repo.DeleteUserAsync(user))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync("test@example.com");

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Användaren togs bort", result.Message);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsCorrectNumberOfUsers()
        {
            // Arrange
            var fakeUser = new List<ApplicationUser>
            {
                new ApplicationUser { Id="1", Email="one@example.com", UserFirstName="Anna", UserLastName="Svensson"},
                new ApplicationUser { Id="2", Email="two@example.com", UserFirstName="Erik", UserLastName="Karlsson"},
            };

            _userRepoMock.Setup( repo => repo.GetAllUsersAsync())
                .ReturnsAsync(fakeUser);

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);

            _userRepoMock.Setup(repo => repo.GetRolesByIdAsync(It.IsAny<string>()))
             .ReturnsAsync(new List<string> { "User" }); // eller tom lista

            //Act
            var result = await service.GetAllUsersAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var user1 = result.First();
            Assert.Equal("one@example.com",  user1.Email);
            Assert.Equal("Anna", user1.FirstName);
        }

        [Fact]
        public async Task GetUserByEmailAsync_UserNotFound_ReturnFailResult()
        {
            // Arrange
            string email = "test@example.com";

            _userRepoMock.Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync((ApplicationUser)null!);

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);

            //Act
            var result = await service.GetUserByEmailAsync(email);

            //Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Användaren kunde inte hittas", result.Message);
            Assert.Equal(ErrorType.NotFound, result.Error);
        }

        [Fact]
        public async Task GetUserByEmailAsync_UserFound_ReturnsUserDto()
        {
            //Arrange
            string email = "test@example.com";

            var user = new ApplicationUser
            {
                Id = "1",
                Email = email,
                UserFirstName = "Anna",
                UserLastName = "Svensson",
                UserPhone = "0701234567"
            };

            _userRepoMock.Setup(repo => repo.GetUserByEmailAsync(email)) 
                .ReturnsAsync(user);

            _userRepoMock.Setup(repo => repo.GetRolesByIdAsync(user.Id))
                .ReturnsAsync(new List<string> { "User" });

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);
            //Act
            var result = await service.GetUserByEmailAsync(email);
            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(email, result.Data.Email);
            Assert.Equal("Anna", result.Data.FirstName);
            Assert.Contains("User", result.Data.Roles);
        }

        [Fact]
        public async Task CreateUserAsync_Success_ReturnsOk()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "test@example.com",
                Password = "SecurePass123!",
                Role = "User"
            };

            var createdUser = new ApplicationUser
            {
                Id = "123",
                Email = dto.Email
            };

            _userRepoMock.Setup(repo => repo.CreateUserAsync(It.IsAny<ApplicationUser>(), dto.Password))
                         .ReturnsAsync((IdentityResult.Success, createdUser));

            _userRepoMock.Setup(repo => repo.AddUserToRoleAsync(createdUser.Id, dto.Role))
                         .ReturnsAsync(true);

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);

            // Act
            var result = await service.CreateUserAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Användaren är skapad", result.Message);

            _auditLoggerMock.Verify(a => a.LogAsync(
                "CreateUserSuccess", "[System/Admin]", dto.Email,
                It.Is<string>(d => d.Contains("User created"))
            ), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_CreateFails_ReturnsFail()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "fail@example.com",
                Password = "SecurePass123!",
                Role = "User"
            };

            var user = new ApplicationUser { Email = dto.Email };

            _userRepoMock.Setup(repo => repo.CreateUserAsync(It.IsAny<ApplicationUser>(), dto.Password))
                         .ReturnsAsync((IdentityResult.Failed(new IdentityError { Description = "Error" }), user));

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);

            // Act
            var result = await service.CreateUserAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Användaren kunde inte skapas", result.Message);

            _auditLoggerMock.Verify(a => a.LogAsync(
                "CreateUserFailed", "[System/Admin]", dto.Email,
                It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_RoleAssignmentFails_ReturnsFail()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "rolefail@example.com",
                Password = "SecurePass123!",
                Role = "Admin"
            };

            var createdUser = new ApplicationUser
            {
                Id = "456",
                Email = dto.Email
            };

            _userRepoMock.Setup(repo => repo.CreateUserAsync(It.IsAny<ApplicationUser>(), dto.Password))
                         .ReturnsAsync((IdentityResult.Success, createdUser));

            _userRepoMock.Setup(repo => repo.AddUserToRoleAsync(createdUser.Id, dto.Role))
                         .ReturnsAsync(false);

            var service = new UserService(_userRepoMock.Object, _auditLoggerMock.Object);

            // Act
            var result = await service.CreateUserAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Kunde inte skapa en roll till användaren", result.Message);

            _auditLoggerMock.Verify(a => a.LogAsync(
                "AddUserToRoleFailed", "[System/Admin]", dto.Email,
                It.Is<string>(d => d.Contains("could not be assigned"))
            ), Times.Once);
        }
    }
}
