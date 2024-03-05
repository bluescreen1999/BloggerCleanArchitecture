using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Blogs.Commands.Delete;

public sealed class DeleteBlogService : IDeleteBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBlogService(
        IBlogRepository blogRepository,
        IUnitOfWork unitOfWork)
    {
        _blogRepository = blogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.FindById(id, cancellationToken);

        if (blog is null)
            throw new NotFoundException(nameof(Blog), id);

        blog.IsDeleted = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

