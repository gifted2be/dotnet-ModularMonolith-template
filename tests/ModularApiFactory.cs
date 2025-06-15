using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace ModularMonolith.Template.Application.Tests
{
    public class ModularApiFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // You can inject mock services or In-Memory DB for testing here
            });

            builder.ConfigureAppConfiguration((context, config) =>
            {
                // You can configure additional test environment settings here
            });

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
        }
    }
}
