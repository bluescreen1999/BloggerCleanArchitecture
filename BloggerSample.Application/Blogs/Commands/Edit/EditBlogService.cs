using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Blogs.Commands.Edit
{
    public sealed class EditBlogService : IEditBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public EditBlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> Execute(
            EditBlogDto editBlogDto,
            Guid id,
            CancellationToken cancellationToken)
        {
            await GuardAgainstDuplicateTitle(editBlogDto, cancellationToken);

            var affectedRows = await _blogRepository.Edit(id, cancellationToken, editBlogDto);

            if (affectedRows == 0)
                throw new NotFoundException(nameof(Blog), id);

            return true;
        }

        private async Task GuardAgainstDuplicateTitle(
            EditBlogDto editBlogDto,
            CancellationToken cancellationToken)
        {
            if (await _blogRepository.IsTitleDuplicate(editBlogDto.title, cancellationToken))
                throw new DuplicateTitleException(editBlogDto.title);
        }
    }
}
