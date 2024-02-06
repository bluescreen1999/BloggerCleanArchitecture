using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;
using BloggerSample.Infrastructure.Services;
using Moq;

namespace BloggerSample.TestTools.Blogs.Edit
{
    public sealed class EditBlogServiceFactory
    {
        public static IEditBlogService GenerateService(
            ApplicationDbContext context)
        {
            var paginationService = new Mock<PaginationService<GetAllBlogsDto>>();
            var blogRepository = new BlogRepository(context, paginationService.Object);
            var unitOfWork = new UnitOfWork(context);

            return new EditBlogService(blogRepository, unitOfWork);
        }
    }
}
