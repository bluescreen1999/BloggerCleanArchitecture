using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Infrastructure.Repositories;
using BloggerSample.Infrastructure;
using BloggerSample.Application.Blogs.Queries.GetDetails;

namespace BloggerSample.TestTools.Blogs.GetDetails
{
    public sealed class GetBlogDetailsServiceFactory
    {
        public static IGetBlogDetailsService GenerateService(
            ApplicationDbContext context)
        {
            var blogRepository = new BlogRepository(context);

            return new GetBlogDetailsService(blogRepository);
        }
    }
}
