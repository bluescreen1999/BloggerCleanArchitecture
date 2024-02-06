using BloggerSample.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BloggerSample.Tests
{
    public class TestsConfigurations : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        protected readonly ApplicationDbContext _dbContext;

        public TestsConfigurations()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
            _dbContext = new ApplicationDbContext(_options);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}