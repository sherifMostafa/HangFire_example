using Hangfire.Server;
using HangFire.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Domains.Task> Tasks { get; set; }

        // Optionally override OnModelCreating to configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
