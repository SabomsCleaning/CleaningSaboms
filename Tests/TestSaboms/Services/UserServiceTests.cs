using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using CleaningSaboms.Services;
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
    }
}
