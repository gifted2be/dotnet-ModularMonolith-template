using ModularMonolith.Template.SharedKernel.Auth;

namespace Auth.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(JwtUserInfo userInfo);
        string GenerateRefreshToken(JwtUserInfo user);
        JwtUserInfo? ValidateToken(string token);
        bool IsRefreshToken(string token);
    }
}
