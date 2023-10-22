using MediatR;

namespace BloggerSample.Application.Blogs.Commands.Edit
{
    public sealed class EditBlogCommandHandler 
        : IRequestHandler<EditBlogCommand, bool>
    {
        private readonly IEditBlogService _editBlogService;

        public EditBlogCommandHandler(IEditBlogService editBlogService)
        {
            _editBlogService = editBlogService;
        }

        public async Task<bool> Handle(
            EditBlogCommand request,
            CancellationToken cancellationToken)
        {
            return await _editBlogService.Execute(request.EditBlogDto, request.Id, cancellationToken);
        }
    }
}
