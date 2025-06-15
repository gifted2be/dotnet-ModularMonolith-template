using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Template.Infra.Interfaces;
using ModularMonolith.Template.SharedKernel.Helpers;
using Users.Application.DTOs;
using Users.Application.Interfaces;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerService _logger;

        public UserController(IUserService userService, ILoggerService logger) {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() {
            _logger.LogInfo("UserController GetAllUsers called");
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();

            return Ok(ResponseHelper.Success(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            UserDto? user = await _userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return Ok(ResponseHelper.NoContent());
            }

            return Ok(ResponseHelper.Success(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, ResponseHelper.Success(userDto, 201));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserDto userDto)
        {
            if (id != userDto.Id) {
                return BadRequest(ResponseHelper.BadRequest("Query Parameter doesn't match the body property value"));
            }

            await _userService.UpdateUserAsync(userDto);

            return Ok(ResponseHelper.NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(ResponseHelper.NoContent());
        }
    }
}
