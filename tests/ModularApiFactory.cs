using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularMonolith.Template.Config.DbContext;
using Users.Domain.Entities;
using Users.Infra.Entities;

namespace ModularMonolith.Template.Application.Tests
{
    public class ModularApiFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Assume you have your own DbContext, example name is AppDbContext
                ServiceProvider? serviceProvider = services.BuildServiceProvider();

                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    IServiceProvider scopedServices = scope.ServiceProvider;
                    AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();

                    // Ensure the database is created (create tables)
                    db.Database.EnsureCreated();

                    SeedTestUsers(db);
                    // You can also seed test data here
                }
            });

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
        }

        private void SeedTestUsers(AppDbContext db)
        {
            if (!db.Set<UserEntity>().Any())
            {
                db.Set<UserEntity>().Add(new UserEntity
                {
                    Id = Guid.NewGuid(),
                    UserName = "testuser",
                    Email = "test@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123")
                });
                db.SaveChanges();
            }
        }
    }
}
