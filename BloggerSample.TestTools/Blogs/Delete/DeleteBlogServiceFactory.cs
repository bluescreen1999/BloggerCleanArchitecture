using BloggerSample.Application.Blogs.Commands.Delete;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Infrastructure;
using BloggerSample.Infrastructure.Repositories;
using BloggerSample.Infrastructure.Services;
using Moq;

namespace BloggerSample.TestTools.Blogs.Delete
{
    public sealed class DeleteBlogServiceFactory
    {
        public static IDeleteBlogService GenerateService(
            ApplicationDbContext context)
        {
            var paginationService = new Mock<PaginationService<GetAllBlogsDto>>();
            var blogRepository = new BlogRepository(context, paginationService.Object);
            var unitOfWork = new UnitOfWork(context);

            return new DeleteBlogService(blogRepository, unitOfWork);
        }
    }
}
