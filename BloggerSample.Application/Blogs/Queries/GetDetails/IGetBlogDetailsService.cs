using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Queries.GetDetails
{
    public interface IGetBlogDetailsService : IService
    {
        Task<GetBlogDetailsDto> Execute(Guid id, CancellationToken cancellationToken);
    }
}
