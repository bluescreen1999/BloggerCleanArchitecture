using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Add
{
    public sealed class AddBlogCommandHandler 
        : IRequestHandler<AddBlogCommand, Guid>
    {
        private readonly IAddBlogService _addBlogService;

        public AddBlogCommandHandler(IAddBlogService addBlogService)
        {
            _addBlogService = addBlogService;
        }

        public async Task<Guid> Handle(
            AddBlogCommand request, 
            CancellationToken cancellationToken)
        {
            return await _addBlogService.Execute(request.AddBlogDto, cancellationToken);
        }
    }
}
