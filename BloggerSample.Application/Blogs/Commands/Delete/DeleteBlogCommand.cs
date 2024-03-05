using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Delete;

public sealed class DeleteBlogCommand : IRequest
{
    public required Guid Id { get; set; }
}

