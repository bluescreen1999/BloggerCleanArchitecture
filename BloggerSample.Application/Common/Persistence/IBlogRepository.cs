using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Common.Persistence
{
    public interface IBlogRepository
    {
        void Add(Blog blog);
        Task<GetBlogDetailsDto> GetDetails(Guid id, CancellationToken cancellationToken);
        Task<bool> IsTitleDuplicate(string title, CancellationToken cancellationToken);
    }
}
