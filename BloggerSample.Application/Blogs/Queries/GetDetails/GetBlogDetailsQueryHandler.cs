using MediatR;

namespace BloggerSample.Application.Blogs.Queries.GetDetails
{
    public sealed class GetBlogDetailsQueryHandler 
        : IRequestHandler<GetBlogDetailsQuery, GetBlogDetailsDto>
    {
        private readonly IGetBlogDetailsService _getBlogDetailsService;

        public GetBlogDetailsQueryHandler(IGetBlogDetailsService getBlogDetailsService)
        {
            _getBlogDetailsService = getBlogDetailsService;
        }

        public async Task<GetBlogDetailsDto> Handle(
            GetBlogDetailsQuery request,
            CancellationToken cancellationToken)
        {
            return await _getBlogDetailsService.Execute(request.Id, cancellationToken);
        }
    }
}
