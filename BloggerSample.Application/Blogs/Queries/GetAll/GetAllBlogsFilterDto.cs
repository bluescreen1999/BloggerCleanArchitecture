using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Queries.GetAll;

public sealed class GetAllBlogsFilterDto
{
    public PagingParams PagingParams { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public bool? IsDeleted { get; set; }
}
