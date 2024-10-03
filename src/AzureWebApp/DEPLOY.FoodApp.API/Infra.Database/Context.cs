using DEPLOY.FoodApp.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace DEPLOY.FoodApp.API.Infra.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
