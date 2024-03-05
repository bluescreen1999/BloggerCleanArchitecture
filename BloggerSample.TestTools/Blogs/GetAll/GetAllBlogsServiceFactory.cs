using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;

namespace BloggerSample.TestTools.Blogs.GetAll
{
    public sealed class GetAllBlogsServiceFactory
    {
        public static IGetAllBlogsService GenerateService(
            ApplicationDbContext context)
        {
            var blogRepository = new BlogRepository(context);

            return new GetAllBlogsService(blogRepository);
        }
    }
}
