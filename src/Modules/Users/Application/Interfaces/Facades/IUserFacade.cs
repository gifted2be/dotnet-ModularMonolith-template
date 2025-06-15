using Users.Application.DTOs;

namespace Users.Application.Interfaces.Facades
{
    public interface IUserFacade
    {
        Task<UserDto?> ValidateUserAsync(string email, string password = "");
        Task RegisterUserAsync(UserDto userDto);
        Task<bool> IsEmailAvailableAsync(string email);
    }
}
