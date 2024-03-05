using BloggerSample.Application.Common.Models;
using BloggerSample.Application.Common.Persistence;

namespace BloggerSample.Application.Blogs.Queries.GetAll;

public sealed class GetAllBlogsService : IGetAllBlogsService
{
    private readonly IBlogRepository _blogRepository;

    public GetAllBlogsService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<PagedList<GetAllBlogsDto>> Execute(
        PagingParams pagingParams,
        GetAllBlogsFilterDto? filterDto,
        CancellationToken cancellationToken)
    {
        return await _blogRepository.GetAll(pagingParams, filterDto, cancellationToken);
    }
}
