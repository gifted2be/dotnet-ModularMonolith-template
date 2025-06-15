using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularMonolith.Template.Application.Tests.Common;
using ModularMonolith.Template.Config.DbContext;

using Users.Infra.Entities;

namespace ModularMonolith.Template.Application.Tests
{
    public class ModularApiFactory : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection;
        private static bool _isInitialized = false;
        private static readonly object _lock = new();

        public ModularApiFactory()
        {
            _connection = new SqliteConnection("DataSource=file:memdb1?mode=memory&cache=shared");
            _connection.Open(); 
        }

        //protected override IHost CreateHost(IHostBuilder builder)
        //{
        //    //builder.ConfigureServices(services =>
        //    //{
        //    //    // Assume you have your own DbContext, example name is AppDbContext
        //    //    ServiceProvider? serviceProvider = services.BuildServiceProvider();

        //    //    using (IServiceScope scope = serviceProvider.CreateScope())
        //    //    {
        //    //        IServiceProvider scopedServices = scope.ServiceProvider;
        //    //        AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();

        //    //        db.Database.EnsureDeleted();
        //    //        // Ensure the database is created (create tables)
        //    //        db.Database.EnsureCreated();

        //    //        SeedTestUsers(db);
        //    //        // You can also seed test data here
        //    //    }
        //    //});

        //    //return base.CreateHost(builder);                     
        //}

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add new DbContext using shared connection
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                // ⚠️ 
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        db.Database.EnsureCreated();
                        SeedTestUsers(db);
                        _isInitialized = true;
                    }
                }
            });

            builder.UseContentRoot(Directory.GetCurrentDirectory());
        }

        private void SeedTestUsers(AppDbContext db)
        {
            if (!db.Set<UserEntity>().Any())
            {
                db.Set<UserEntity>().Add(new UserEntity
                {
                    //Id = Guid.Parse("ed7fee6d-1183-480d-a5cc-c607f5cedbf2"),
                    Id = Guid.NewGuid(),
                    UserName = TestUsers.SeedUser.UserName,
                    Email = TestUsers.SeedUser.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(TestUsers.SeedUser.Password)
                });
                db.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Dispose();
        }
    }
}
