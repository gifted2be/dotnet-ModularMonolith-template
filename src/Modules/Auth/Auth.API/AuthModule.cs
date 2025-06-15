using Auth.Application.Interfaces;
using Auth.Application.Mappings;
using Auth.Application.Services;
using Auth.Domain.Interfaces;
using Auth.Infra.IdentityProviders;

namespace Auth.API
{
    public static class AuthModule
    {
        public static void RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IIdentityProviderService, GoogleAuthService>();
            services.AddScoped<IIdentityProviderService, FacebookAuthService>();
            services.AddAutoMapper(typeof(AuthMappingProfile));
        }
    }
}
