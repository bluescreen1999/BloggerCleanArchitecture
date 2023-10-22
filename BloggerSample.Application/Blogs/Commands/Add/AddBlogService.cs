using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Common.Interfaces;
using BloggerSample.Application.Common.Persistence;
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
            if (await _blogRepository.IsTitleDuplicate(addBlogDto.title, cancellationToken))
                throw new DuplicateTitleException(addBlogDto.title);

            var currentDateTime = _dateTimeOffsetProvider.UtcNow;

            Blog blog = new()
            {
                Id = Guid.NewGuid(),
                Body = addBlogDto.title,
                IsDeleted = false,
                Title = addBlogDto.title,
                CreationDateTime = currentDateTime
            };

            _blogRepository.Add(blog);

            await _unitOfWork.SaveChangesAsync();

            return blog.Id;
        }
    }
}
