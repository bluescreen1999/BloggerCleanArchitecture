using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Application.Common.Models;
using BloggerSample.TestTools.Blogs;
using BloggerSample.TestTools.Blogs.GetAll;

namespace BloggerSample.Tests.BlogServicesTests.GetAll
{
    public sealed class GetAllBlogsServiceTests : TestsConfigurations
    {
        private readonly IGetAllBlogsService _sut;

        public GetAllBlogsServiceTests()
        {
            _sut = GetAllBlogsServiceFactory
                .GenerateService(_dbContext);
        }

        [Fact]
        public async Task Execute_gets_all_blogs_properly()
        {
            var blog1 = new BlogBuilder()
                .WithTitle("Dummy blog title 1")
                .WithBody("Dummy blog body 1")
                .WithCreationDateTimeOffset(new DateTimeOffset(1990, 5, 7, 10, 40, 0, TimeSpan.Zero))
                .WithIsDeleted(false)
                .Build();
            _dbContext.Add(blog1);
            var blog2 = new BlogBuilder()
                .WithTitle("Dummy blog title 2")
                .WithBody("Dummy blog body 2")
                .WithCreationDateTimeOffset(new DateTimeOffset(2010, 7, 5, 11, 10, 0, TimeSpan.Zero))
                .WithIsDeleted(true)
                .Build();
            _dbContext.Add(blog2);
            var blog3 = new BlogBuilder()
                .WithTitle("Dummy blog title 3")
                .WithBody("Dummy blog body 3")
                .WithCreationDateTimeOffset(new DateTimeOffset(2020, 2, 4, 19, 30, 0, TimeSpan.Zero))
                .WithIsDeleted(false)
                .Build();
            _dbContext.Add(blog3);
            await _dbContext.SaveChangesAsync();

            var expected = await _sut
                .Execute(new PagingParams { PageNumber = 1, PageSize = 3 },
                null,
                CancellationToken.None);
            expected.TotalItems.Should().Be(3);
            expected.Items.First().Title.Should().Be(blog3.Title);
            expected.Items.First().Id.Should().Be(blog3.Id);
            expected.Items.Last().Title.Should().Be(blog1.Title);
            expected.Items.Last().Id.Should().Be(blog1.Id);
        }
    }
}
