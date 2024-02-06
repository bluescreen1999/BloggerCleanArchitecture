using BloggerSample.Application.Blogs.Commands.Delete;
using BloggerSample.TestTools.Blogs.Delete;
using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Domain.Entities;
using BloggerSample.TestTools.Blogs;

namespace BloggerSample.Tests.BlogServicesTests.Delete
{
    public sealed class DeleteBlogServiceTests : TestsConfigurations
    {
        private IDeleteBlogService _sut;

        public DeleteBlogServiceTests()
        {
            _sut = DeleteBlogServiceFactory
                .GenerateService(_dbContext);
        }

        [Fact]
        public async Task Execute_deletes_blog_properly()
        {
            var blog1 = new BlogBuilder()
                .WithTitle("Dummy Blog Title1")
                .WithBody("Dummy Blog Body1")
                .WithIsDeleted(false)
                .Build();
            _dbContext.Add(blog1);
            var blog2 = new BlogBuilder()
                .WithTitle("Dummy Blog Title2")
                .WithBody("Dummy Blog Body2")
                .WithIsDeleted(false)
                .Build();
            _dbContext.Add(blog2);
            await _dbContext.SaveChangesAsync();

            await _sut.Execute(blog2.Id, CancellationToken.None);

            var expected = await _dbContext.Set<Blog>().ToListAsync();
            var expectedBlog1 = expected.Where(_ => _.Id == blog1.Id).Single();
            var expectedBlog2 = expected.Where(_ => _.Id == blog2.Id).Single();
            expectedBlog1.IsDeleted.Should().BeFalse();
            expectedBlog1.Title.Should().Be(blog1.Title);
            expectedBlog1.Body.Should().Be(blog1.Body);
            expectedBlog1.CreationDateTime.Should().Be(blog1.CreationDateTime);
            expectedBlog2.IsDeleted.Should().BeTrue();
            expectedBlog2.Title.Should().Be(blog2.Title);
            expectedBlog2.Body.Should().Be(blog2.Body);
            expectedBlog2.CreationDateTime.Should().Be(blog2.CreationDateTime);
        }

        [Fact]
        public async Task Execute_throws_exception_when_blog_not_found()
        {
            var invalidBlogId = Guid.Empty;

            Func<Task> expected = async () => await _sut
            .Execute(invalidBlogId, CancellationToken.None);

            await expected.Should()
                .ThrowExactlyAsync<NotFoundException>();
            expected.Invoke().Exception!.InnerException!.Message.Should()
                .Be($"{nameof(Blog)} With id: {invalidBlogId} Not Found");
        }
    }
}
