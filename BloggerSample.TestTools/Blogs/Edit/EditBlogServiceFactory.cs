using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;

namespace BloggerSample.TestTools.Blogs.Edit
{
    public sealed class EditBlogServiceFactory
    {
        public static IEditBlogService GenerateService(
            ApplicationDbContext context)
        {
            var blogRepository = new BlogRepository(context);
            var unitOfWork = new UnitOfWork(context);

            return new EditBlogService(blogRepository, unitOfWork);
        }
    }
}
