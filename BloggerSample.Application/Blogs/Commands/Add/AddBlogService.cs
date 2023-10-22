using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Application.Common.Interfaces;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Blogs.Commands.Add
{
    public sealed class AddBlogService : IAddBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AddBlogService(
            IBlogRepository blogRepository,
            IDateTimeOffsetProvider dateTimeOffsetProvider,
            IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _dateTimeOffsetProvider = dateTimeOffsetProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Execute(
            AddBlogDto addBlogDto,
            CancellationToken cancellationToken)
        {
            await GuardAgainstDuplicateTitle(addBlogDto, cancellationToken);

            var currentDateTime = _dateTimeOffsetProvider.UtcNow;
            Blog blog = InitializeBlog(addBlogDto, currentDateTime);

            _blogRepository.Add(blog);

            await _unitOfWork.SaveChangesAsync();

            return blog.Id;
        }

        private async Task GuardAgainstDuplicateTitle(
            AddBlogDto addBlogDto,
            CancellationToken cancellationToken)
        {
            if (await _blogRepository.IsTitleDuplicate(addBlogDto.title, cancellationToken))
                throw new DuplicateTitleException(addBlogDto.title);
        }

        private static Blog InitializeBlog(
            AddBlogDto addBlogDto,
            DateTimeOffset currentDateTime)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Title = addBlogDto.title,
                Body = addBlogDto.body,
                IsDeleted = false,
                CreationDateTime = currentDateTime
            };
        }
    }
}
