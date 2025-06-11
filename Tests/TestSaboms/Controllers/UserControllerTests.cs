using CleaningSaboms.Controllers;
using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestSaboms.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOk_WithListOfUsers()
        {
            // Arrange
            var fakeUsers = new List<UserDto>
            {
                new UserDto { Email = "user1@example.com", FirstName = "User One" },
                new UserDto { Email = "user2@example.com", FirstName = "User Two" }
            };

            _userServiceMock.Setup(service => service.GetAllUsersAsync())
                            .ReturnsAsync(fakeUsers);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(2, ((List<UserDto>)returnedUsers).Count);
        }

        [Fact]
        public async Task CreateUser_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Email", "Required");

            var result = await _controller.CreateUser(new RegisterUserDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_Failure_ReturnsBadRequest()
        {
            var dto = new RegisterUserDto { Email = "test@example.com" };
            _userServiceMock
                .Setup(s => s.CreateUserAsync(dto))
                .ReturnsAsync(new ServiceResult<UserDto> { Success = false, Message = "Error" });

            var result = await _controller.CreateUser(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequest.Value);
        }


        //TODO: Kolla upp denna inte korrekt
        //[Fact]
        //public async Task CreateUser_Success_ReturnsOk()
        //{
        //    var dto = new RegisterUserDto { Email = "test@example.com" };
        //    _userServiceMock
        //        .Setup(s => s.CreateUserAsync(dto))
        //        .ReturnsAsync(new ServiceResult<UserDto> { Success = true });

        //    var result = await _controller.CreateUser(dto);

        //    var okResult = Assert.IsType<OkObjectResult>(result);

        //    // Använd dynamic för att läsa "message" från det anonyma objektet
        //    dynamic value = okResult.Value;
        //    Assert.Equal("User created successfully", value.message);
        //}

        [Fact]
        public async Task GetAllUsers_ReturnsOk()
        {
            var users = new List<UserDto> { new() { Email = "a@example.com" } };
            _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _controller.GetAllUsers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetUserByEmail_NotFound()
        {
            _userServiceMock.Setup(s => s.GetUserByEmailAsync("missing@example.com"))
            .ReturnsAsync(new ServiceResult<UserDto> { Success = false, Data = null });
            var result = await _controller.GetUserByEmail("missing@example.com");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserByEmail_Found_ReturnsOk()
        {
            var user = new UserDto { Email = "found@example.com" };
            _userServiceMock.Setup(s => s.GetUserByEmailAsync("found@example.com"))
                .ReturnsAsync(new ServiceResult<UserDto> { Success= true, Data = user});

            var result = await _controller.GetUserByEmail("found@example.com");

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(user.Email, "found@example.com");
        }

        [Fact]
        public async Task DeleteUser_Success_ReturnsOk()
        {
            var resultObj = new ServiceResult<bool> { Success = true, Data=true };
            _userServiceMock
                .Setup(s => s.DeleteUserAsync("test@example.com"))
                .ReturnsAsync(resultObj);

            var result = await _controller.DeleteUser("test@example.com");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultObj, okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_NotFound_ReturnsNotFound()
        {
            var serviceResult = new ServiceResult<bool>
            {
                Success = false,
                Message = "Användaren kunde inte hittas",
                Error = ErrorType.NotFound
            };

            _userServiceMock
                .Setup(s => s.DeleteUserAsync("nobody@example.com"))
                .ReturnsAsync(serviceResult);

            var result = await _controller.DeleteUser("nobody@example.com");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnedValue = Assert.IsType<ServiceResult<bool>>(notFoundResult.Value);

            Assert.False(returnedValue.Success);
            Assert.Equal("Användaren kunde inte hittas", returnedValue.Message);
            Assert.Equal(ErrorType.NotFound, returnedValue.Error);
        }


        [Fact]
        public async Task UpdateUser_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.UpdateUser(new UpdateUserDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_Failure_ReturnsBadRequest()
        {
            var dto = new UpdateUserDto { Email = "test@example.com" };
            _userServiceMock.Setup(s => s.UpdateUserAsync(dto)).ReturnsAsync(new ServiceResult { Success = false, Message = "Update failed" });

            var result = await _controller.UpdateUser(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Update failed", badRequest.Value);
        }

        [Fact]
        public async Task UpdateUser_Success_ReturnsOk()
        {
            var dto = new UpdateUserDto { Email = "test@example.com" };
            _userServiceMock
                .Setup(s => s.UpdateUserAsync(dto))
                .ReturnsAsync(ServiceResult.Ok("Användaren är uppdaterad"));

            var result = await _controller.UpdateUser(dto);

            var okResult = Assert.IsType<OkObjectResult>(result); 
            var serviceResult = Assert.IsType<ServiceResult>(okResult.Value); 
            Assert.Equal("Användaren är uppdaterad", serviceResult.Message);
        }
    }
}

