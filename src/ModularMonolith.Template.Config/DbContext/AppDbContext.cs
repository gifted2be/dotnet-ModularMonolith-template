using Microsoft.EntityFrameworkCore;
using ModularMonolith.Template.Config.Loader;
using MSDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ModularMonolith.Template.Config.DbContext
{
    public class AppDbContext: MSDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModuleEntityLoader.ApplyModuleEntityConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
