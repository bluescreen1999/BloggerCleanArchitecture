using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Common.Models;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Common.Persistence
{
    public interface IBlogRepository : IRepository
    {
        void Add(Blog blog);
        Task<int> Delete(Guid id, CancellationToken cancellationToken);
        Task<int> Edit(Guid id, CancellationToken cancellationToken, EditBlogDto editBlogDto);
        Task<PagedList<GetAllBlogsDto>> GetAll(PagingParams pagingParams, GetAllBlogsFilterDto? filterDto, CancellationToken cancellationToken);
        Task<GetBlogDetailsDto> GetDetails(Guid id, CancellationToken cancellationToken);
        Task<bool> IsTitleDuplicate(string title, CancellationToken cancellationToken);
        Task<bool> IsTitleDuplicate(string title, Guid id, CancellationToken cancellationToken);
        Task<Blog> FindById(Guid id, CancellationToken cancellationToken);
    }
}
