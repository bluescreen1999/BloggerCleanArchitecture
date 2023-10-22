using BloggerSample.Application.Common.Persistence;

namespace BloggerSample.Application.Blogs.Queries.GetDetails
{
    public interface IGetBlogDetailsService
    {
        Task<GetBlogDetailsDto> Execute(Guid id, CancellationToken cancellationToken);
    }

    public sealed class GetBlogDetailsService : IGetBlogDetailsService
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogDetailsService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<GetBlogDetailsDto> Execute(Guid id, CancellationToken cancellationToken)
        {
            return await _blogRepository.GetDetails(id, cancellationToken);
        }
    }
}
