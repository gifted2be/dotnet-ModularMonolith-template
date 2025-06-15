using Users.Application.Facades;
using Users.Application.Interfaces;
using Users.Application.Interfaces.Facades;
using Users.Application.Mappings;
using Users.Application.Services;
using Users.Domain.Interfaces;
using Users.Infra.Repositories;

namespace Users.API
{
    public static class UserModule
    {
        public static void RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddAutoMapper(typeof(UserMappingProfile));
        }
    }
}
