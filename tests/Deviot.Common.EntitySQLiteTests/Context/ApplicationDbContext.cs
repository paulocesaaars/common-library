using Deviot.Common.EntitySQLiteTests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deviot.Common.EntitySQLiteTests.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            Seed.Create(modelBuilder);
        }
    }
}
