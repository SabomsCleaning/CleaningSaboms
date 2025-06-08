using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleaningSaboms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateUser request.");
                return BadRequest(ModelState);
            }
            var result = await _userService.CreateUserAsync(dto);

            if (!result.Success)
            {
                _logger.LogError("Failed to create user: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(new { message = "User created successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            if (!result.Success)
                return NotFound();
            return Ok(result.Data);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await _userService.DeleteUserAsync(email);
            
            if (result.Success)
                return Ok(result);

            if (result.Error == Results.ErrorType.NotFound)
                return NotFound(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task <IActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateUser request.");
                return BadRequest(ModelState);
            }
            var result = await _userService.UpdateUserAsync(user);
            if (!result.Success)
            {
                _logger.LogError("Failed to update user: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }

}
