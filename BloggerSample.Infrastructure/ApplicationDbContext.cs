using Microsoft.EntityFrameworkCore;

namespace BloggerSample.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder _)
        {
            base.OnConfiguring(_);
        }

        protected override void OnModelCreating(ModelBuilder _)
        {
            base.OnModelCreating(_);
            _.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}
