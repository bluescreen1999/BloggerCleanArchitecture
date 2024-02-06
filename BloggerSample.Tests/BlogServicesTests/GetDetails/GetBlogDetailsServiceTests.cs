using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.TestTools.Blogs;
using BloggerSample.TestTools.Blogs.GetDetails;

namespace BloggerSample.Tests.BlogServicesTests.GetDetails
{
    public sealed class GetBlogDetailsServiceTests : TestsConfigurations
    {
        private readonly IGetBlogDetailsService _sut;

        public GetBlogDetailsServiceTests()
        {
            _sut = GetBlogDetailsServiceFactory
                .GenerateService(_dbContext);
        }

        [Fact]
        public async Task Execute_gets_blog_details_properly()
        {
            var blog = new BlogBuilder()
                .WithTitle("First Dummy Blog Title")
                .WithBody("First Dummy Blog Body")
                .WithIsDeleted(false)
                .WithCreationDateTimeOffset(DateTimeOffset.UtcNow)
                .Build();
            _dbContext.Add(blog);
            await _dbContext.SaveChangesAsync();

            var expected = await _sut.Execute(blog.Id, CancellationToken.None);

            expected.Title.Should().Be(blog.Title);
            expected.Body.Should().Be(blog.Body);
            expected.IsDeleted.Should().Be(blog.IsDeleted);
            expected.CreationDateTime.Should().Be(blog.CreationDateTime);
        }
    }
}
