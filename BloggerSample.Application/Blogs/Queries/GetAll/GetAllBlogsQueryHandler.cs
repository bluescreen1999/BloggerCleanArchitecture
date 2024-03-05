using BloggerSample.Application.Common.Models;
using MediatR;

namespace BloggerSample.Application.Blogs.Queries.GetAll;

public sealed class GetAllBlogsQueryHandler
    : IRequestHandler<GetAllBlogsQuery, PagedList<GetAllBlogsDto>>
{
    private readonly IGetAllBlogsService _getAllBlogsService;

    public GetAllBlogsQueryHandler(IGetAllBlogsService getAllBlogsService)
    {
        _getAllBlogsService = getAllBlogsService;
    }

    public async Task<PagedList<GetAllBlogsDto>> Handle(
        GetAllBlogsQuery request,
        CancellationToken cancellationToken)
    {
        return await _getAllBlogsService.Execute(
            request.PagingParams,
            request.FilterDto,
            cancellationToken);
    }
}
