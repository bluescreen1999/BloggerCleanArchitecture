using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Commands.Add;

public interface IAddBlogService: IService
{
    Task<Guid> Execute(AddBlogDto addBlogDto, CancellationToken cancellationToken);
}

