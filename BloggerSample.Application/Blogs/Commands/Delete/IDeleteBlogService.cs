using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Commands.Delete;

public interface IDeleteBlogService : IService
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}

