using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Infrastructure.Services;
using BloggerSample.TestTools.Blogs.Add;
using BloggerSample.Domain.Entities;
using BloggerSample.TestTools.Blogs;

namespace BloggerSample.Tests.BlogServicesTests.Add
{
    public sealed class AddBlogServiceTests : TestsConfigurations
    {
        private readonly IAddBlogService _sut;
        private readonly DateTimeOffsetProvider _dateTimeOffsetProvider;
        private readonly DateTimeOffset _currentDateTimeOffset;

        public AddBlogServiceTests()
        {
            _dateTimeOffsetProvider = new DateTimeOffsetProvider();
            _currentDateTimeOffset = _dateTimeOffsetProvider.UtcNow;
            _sut = AddBlogServiceFactory
                .GenerateService(_dbContext, _currentDateTimeOffset);
        }

        [Fact]
        public async Task Execute_adds_blog_properly()
        {
            var dto = new AddBlogDtoBuilder()
                .Build();

            var actual = await _sut.Execute(dto, CancellationToken.None);

            var expected = await _dbContext.Set<Blog>().FirstAsync();
            expected.Title.Should().Be(dto.title);
            expected.Body.Should().Be(dto.body);
            expected.CreationDateTime.Should().Be(_currentDateTimeOffset);
            expected.Id.Should().Be(actual);
            expected.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public async Task Execute_adds_only_one_blog_properly()
        {
            var dto = new AddBlogDtoBuilder()
                .Build();

            await _sut.Execute(dto, CancellationToken.None);

            var expected = await _dbContext.Set<Blog>().ToListAsync();
            expected.Count().Should().Be(1);
        }

        [Fact]
        public async Task Execute_throws_exception_when_blog_title_is_duplicate()
        {
            var duplicateTitle = "Duplicate Dummy Title";
            var blog = new BlogBuilder()
                .WithTitle(duplicateTitle)
                .Build();
            _dbContext.Add(blog);
            await _dbContext.SaveChangesAsync();
            var dto = new AddBlogDtoBuilder()
                .WithTitle(duplicateTitle)
                .Build();

            Func<Task> expected = async () => await _sut
            .Execute(dto, CancellationToken.None);

            await expected.Should()
                .ThrowExactlyAsync<DuplicateTitleException>();
            expected.Invoke().Exception!.InnerException!.Message.Should()
                .Be($"Blog with {duplicateTitle} title already exists");
        }
    }
}
