using Hangfire.Server;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Data
{
    public class AppDbContext: DbContext
    {

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            // Optionally override OnModelCreating to configure the model
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

            }
    }
}
