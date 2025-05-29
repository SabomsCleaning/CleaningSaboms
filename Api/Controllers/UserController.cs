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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateUser request.");
                return BadRequest(ModelState);
            }
            var result = await _userService.CreateUserAsync(dto); // Replace with actual DTO and password handling
            // Logic to create a user
            if (!result.Success)
            {
                _logger.LogError("Failed to create user: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(new { message = "User created successfully" });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await _userService.DeleteUserAsync(email);
            if (result)
                return Ok(result);
            return BadRequest(result);
        }
    }

}
