using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Edit;

public sealed class EditBlogCommand : IRequest<bool>
{
    public required Guid Id { get; init; }
    public required EditBlogDto EditBlogDto { get; set; }

}
