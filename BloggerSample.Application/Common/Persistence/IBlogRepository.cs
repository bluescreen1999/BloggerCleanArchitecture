using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Common.Persistence
{
    public interface IBlogRepository
    {
        void Add(Blog blog);
        Task<bool> IsTitleDuplicate(string title, CancellationToken cancellationToken);
    }
}
