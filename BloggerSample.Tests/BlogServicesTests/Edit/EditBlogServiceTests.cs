using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.TestTools.Blogs.Edit;
using BloggerSample.Domain.Entities;
using BloggerSample.TestTools.Blogs;

namespace BloggerSample.Tests.BlogServicesTests.Edit
{
    public sealed class EditBlogServiceTests : TestsConfigurations
    {
        private readonly IEditBlogService _sut;

        public EditBlogServiceTests()
        {
            _sut = EditBlogServiceFactory.GenerateService(_dbContext);
        }

        [Fact]
        public async Task Execute_edits_blog_properly()
        {
            var blog1 = new BlogBuilder()
                .WithTitle("dummy blog title1")
                .WithBody("dummy blog body1")
                .Build();
            _dbContext.Add(blog1);
            var blog2 = new BlogBuilder()
                .WithTitle("dummy blog2")
                .WithBody("dummy body2")
                .Build();
            _dbContext.Add(blog2);
            await _dbContext.SaveChangesAsync();
            var dto = new EditBlogDtoBuilder()
                .WithTitle("Updated Title")
                .WithBody("Updated Body")
                .Build();

            await _sut.Execute(dto, blog1.Id, CancellationToken.None);

            var expected = await _dbContext.Set<Blog>().ToListAsync();
            var expectedBlog1 = expected.Where(_ => _.Id == blog1.Id).Single();
            var expectedBlog2 = expected.Where(_ => _.Id == blog2.Id).Single();
            expectedBlog1.Title.Should().Be(dto.title);
            expectedBlog1.Body.Should().Be(dto.body);
            expectedBlog1.IsDeleted.Should().Be(blog1.IsDeleted);
            expectedBlog1.CreationDateTime.Should().Be(blog1.CreationDateTime);
            expectedBlog2.Title.Should().Be(blog2.Title);
            expectedBlog2.Body.Should().Be(blog2.Body);
            expectedBlog2.IsDeleted.Should().Be(blog2.IsDeleted);
            expectedBlog2.CreationDateTime.Should().Be(blog2.CreationDateTime);
        }

        [Fact]
        public async Task Execute_throws_exception_when_blog_title_is_duplicate()
        {
            var duplicateBlogTitle = "Dummy Duplicate Title";
            var blog1 = new BlogBuilder()
                .WithTitle(duplicateBlogTitle)
                .Build();
            _dbContext.Add(blog1);
            var blog2 = new BlogBuilder()
                .Build();
            _dbContext.Add(blog2);
            await _dbContext.SaveChangesAsync();
            var dto = new EditBlogDtoBuilder()
                .WithTitle(duplicateBlogTitle)
                .Build();

            Func<Task> expected = async () => await _sut
            .Execute(dto, blog2.Id, CancellationToken.None);

            await expected.Should()
                .ThrowExactlyAsync<DuplicateTitleException>();
            expected.Invoke().Exception!.InnerException!.Message
                .Should().Be($"Blog with {duplicateBlogTitle} title already exists");

        }

        [Fact]
        public async Task Execute_throws_exception_when_blog_not_found()
        {

            var invalidBlogId = Guid.Empty;
            var dto = new EditBlogDtoBuilder()
                .Build();

            Func<Task> expected = async () => await _sut
            .Execute(dto, invalidBlogId, CancellationToken.None);

            await expected.Should()
                .ThrowExactlyAsync<NotFoundException>();
            expected.Invoke().Exception!.InnerException!.Message
                .Should().Be($"{nameof(Blog)} With id: {invalidBlogId} Not Found");
        }
    }
}

