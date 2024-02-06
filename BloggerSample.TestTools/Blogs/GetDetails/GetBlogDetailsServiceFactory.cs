using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Infrastructure.Repositories;
using BloggerSample.Infrastructure;
using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Infrastructure.Services;
using Moq;

namespace BloggerSample.TestTools.Blogs.GetDetails
{
    public sealed class GetBlogDetailsServiceFactory
    {
        public static IGetBlogDetailsService GenerateService(
            ApplicationDbContext context)
        {
            var paginationService = new Mock<PaginationService<GetAllBlogsDto>>();
            var blogRepository = new BlogRepository(context, paginationService.Object);

            return new GetBlogDetailsService(blogRepository);
        }
    }
}
