using Auth.Application.DTOs.RequestModel;
using Auth.Application.DTOs.ResponseModel;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using AutoMapper;
using ModularMonolith.Template.SharedKernel.Auth;
using Users.Application.DTOs;
using Users.Application.Interfaces.Facades;

namespace Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;

        public AuthService(IJwtTokenService jwtTokenService, IUserFacade userFacade, IMapper mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userFacade = userFacade;
            _mapper = mapper;
        }

        public async Task<LoginResposneDto?> Login(LoginDto requestModel)
        {
            UserDto? existingUserDto = await _userFacade.ValidateUserAsync(requestModel.Email, requestModel.Password);

            if (existingUserDto == null) throw AuthException.InvalidCredentials();

            JwtUserInfo jwtUserInfo = _mapper.Map<JwtUserInfo>(existingUserDto);
            string token = _jwtTokenService.GenerateToken(jwtUserInfo);
            string refreshToken = _jwtTokenService.GenerateRefreshToken(jwtUserInfo);

            LoginResposneDto response = new LoginResposneDto();
            response.Token = token;
            response.RefreshToken = refreshToken;
            return response;
        }

        public async Task<RegisterResponseDto?> Register(RegisterDto requestModel)
        {
            bool isEmailAvailable = await _userFacade.IsEmailAvailableAsync(requestModel.Email);
            if (!isEmailAvailable) throw AuthException.EmailAlreadyExists(requestModel.Email);

            UserDto userDto = _mapper.Map<UserDto>(requestModel);

            await _userFacade.RegisterUserAsync(userDto);

            return new RegisterResponseDto
            {
                Email = userDto.Email,
                Message = "Registration successful"
            };

        }

        public async Task<RefreshTokenResponseDto> RefreshAccessToken(RefreshTokenDto requestModel) {
            
            if (!_jwtTokenService.IsRefreshToken(requestModel.RefreshToken))
                throw AuthException.RefreshTokenInvalid();

            JwtUserInfo? jwtUserInfo = _jwtTokenService.ValidateToken(requestModel.RefreshToken) ?? throw AuthException.TokenTampered();
                

            bool isEmailAvailable = await _userFacade.IsEmailAvailableAsync(jwtUserInfo.Email);

            if (isEmailAvailable)
                throw AuthException.UserNotFound(jwtUserInfo.Email);

            string newAccessToken = _jwtTokenService.GenerateToken(jwtUserInfo);
            string newRefreshToken = _jwtTokenService.GenerateRefreshToken(jwtUserInfo);

            return new RefreshTokenResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
