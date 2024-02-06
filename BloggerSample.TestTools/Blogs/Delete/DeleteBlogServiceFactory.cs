using BloggerSample.Application.Blogs.Commands.Delete;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;

namespace BloggerSample.TestTools.Blogs.Delete
{
    public sealed class DeleteBlogServiceFactory
    {
        public static IDeleteBlogService GenerateService(
            ApplicationDbContext context)
        {
            var blogRepository = new BlogRepository(context);
            var unitOfWork = new UnitOfWork(context);

            return new DeleteBlogService(blogRepository, unitOfWork);
        }
    }
}
