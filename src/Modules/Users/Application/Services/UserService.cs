using AutoMapper;
using Users.Application.DTOs;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task CreateUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.AddAsync(user);            
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            IEnumerable<User>? users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            User? user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            User? user = await _userRepository.GetByEmailAsync(email);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password )
        { 
            User? user = await _userRepository.GetByEmailAndPasswordAsync(email, password);

            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            User? user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
        }
    }
}
