using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Application.Common.Interfaces;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;
using Moq;

namespace BloggerSample.TestTools.Blogs.Add
{
    public sealed class AddBlogServiceFactory
    {
        public static IAddBlogService GenerateService(
            ApplicationDbContext context,
            DateTimeOffset currentDateTimeOffset)
        {
            var blogRepository = new BlogRepository(context);
            var dateTimeOffsetProvider = new Mock<IDateTimeOffsetProvider>();
            dateTimeOffsetProvider.Setup(_ => _.UtcNow)
                .Returns(currentDateTimeOffset);
            var unitOfWork = new UnitOfWork(context);

            return new AddBlogService(
                blogRepository,
                dateTimeOffsetProvider.Object,
                unitOfWork);
        }
    }
}
