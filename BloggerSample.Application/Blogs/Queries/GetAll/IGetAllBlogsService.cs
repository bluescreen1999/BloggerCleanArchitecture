using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Queries.GetAll
{
    public interface IGetAllBlogsService : IService
    {
        Task<PagedList<GetAllBlogsDto>> Execute(PagingParams pagingParams, GetAllBlogsFilterDto? filterDto, CancellationToken cancellationToken);
    }
}
