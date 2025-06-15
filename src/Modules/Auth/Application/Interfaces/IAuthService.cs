using Auth.Application.DTOs.RequestModel;
using Auth.Application.DTOs.ResponseModel;

namespace Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResposneDto?> Login(LoginDto requestModel);
        Task<RegisterResponseDto?> Register(RegisterDto requestModel);
        Task<RefreshTokenResponseDto> RefreshAccessToken(RefreshTokenDto requestModel);
    }
}
