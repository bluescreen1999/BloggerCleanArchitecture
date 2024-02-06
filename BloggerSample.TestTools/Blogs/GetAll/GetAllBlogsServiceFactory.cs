using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;
using BloggerSample.Infrastructure.Services;
using Moq;

namespace BloggerSample.TestTools.Blogs.GetAll
{
    public sealed class GetAllBlogsServiceFactory
    {
        public static IGetAllBlogsService GenerateService(
            ApplicationDbContext context)
        {
            var paginationService = new Mock<PaginationService<GetAllBlogsDto>>();
            var blogRepository = new BlogRepository(context, paginationService.Object);

            return new GetAllBlogsService(blogRepository);
        }
    }
}
