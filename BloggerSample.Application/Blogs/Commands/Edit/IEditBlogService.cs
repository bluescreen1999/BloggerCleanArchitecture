using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Commands.Edit
{
    public interface IEditBlogService: IService
    {
        Task<bool> Execute(EditBlogDto editBlogDto, Guid id, CancellationToken cancellationToken);
    }
}
