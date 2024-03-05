using BloggerSample.Application.Common.Models;
using MediatR;

namespace BloggerSample.Application.Blogs.Queries.GetAll;

public sealed class GetAllBlogsQuery : IRequest<PagedList<GetAllBlogsDto>>
{
    public PagingParams PagingParams { get; set; }
    public GetAllBlogsFilterDto? FilterDto { get; set; }
}
