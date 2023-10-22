using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Delete
{
    public sealed class DeleteBlogCommandHandler 
        : IRequestHandler<DeleteBlogCommand>
    {
        private readonly IDeleteBlogService _deleteBlogService;

        public DeleteBlogCommandHandler(IDeleteBlogService deleteBlogService)
        {
            _deleteBlogService = deleteBlogService;
        }

        public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            await _deleteBlogService.Execute(request.Id, cancellationToken);
        }
    }
}
