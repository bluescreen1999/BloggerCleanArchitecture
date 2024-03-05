using MediatR;

namespace BloggerSample.Application.Blogs.Queries.GetDetails;

public sealed class GetBlogDetailsQuery : IRequest<GetBlogDetailsDto>
{
    public required Guid Id { get; init; }
}
