using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Blogs.Commands.Delete
{
    public sealed class DeleteBlogService : IDeleteBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Execute(Guid id, CancellationToken cancellationToken)
        {
            var affectedRows = await _blogRepository.Delete(id, cancellationToken);

            if (affectedRows == 0)
                throw new NotFoundException(nameof(Blog), id);
        }
    }
}
