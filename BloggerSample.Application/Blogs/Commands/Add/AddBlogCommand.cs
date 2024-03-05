using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Add;

public sealed class AddBlogCommand : IRequest<Guid>
{
    public AddBlogDto AddBlogDto { get; set; }
}
