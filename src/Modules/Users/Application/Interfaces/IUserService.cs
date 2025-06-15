using Users.Application.DTOs;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(Guid id);
    }
}
