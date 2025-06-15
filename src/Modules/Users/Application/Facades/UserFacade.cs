using AutoMapper;
using Users.Application.DTOs;
using Users.Application.Exceptions;
using Users.Application.Interfaces;
using Users.Application.Interfaces.Facades;

namespace Users.Application.Facades
{
    public class UserFacade: IUserFacade
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserFacade(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task RegisterUserAsync(UserDto userDto)
        {
            if (!await IsEmailAvailableAsync(userDto.Email))
                throw new UserAlreadyExistsException(userDto.Email);

            await _userService.CreateUserAsync(userDto);
        }

        public async Task<UserDto?> ValidateUserAsync(string email, string password = "")
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            // Try to get user by email only
            UserDto? user = await _userService.GetUserByEmailAsync(email);

            // If found, verify password
            if (user != null && BCrypt.Net.BCrypt.Verify(password.Trim(), user.Password))
                return user;

            return null;
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            UserDto? user = await _userService.GetUserByEmailAsync(email);
            return user == null;
        }

    }
}
