using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Common.Persistence
{
    public interface IBlogRepository
    {
        void Add(Blog blog);
        Task<int> Delete(Guid id, CancellationToken cancellationToken);
        Task<int> Edit(Guid id, CancellationToken cancellationToken, EditBlogDto editBlogDto);
        Task<GetBlogDetailsDto> GetDetails(Guid id, CancellationToken cancellationToken);
        Task<bool> IsTitleDuplicate(string title, CancellationToken cancellationToken);
    }
}
