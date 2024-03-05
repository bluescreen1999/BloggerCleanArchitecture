using BloggerSample.Application.Common.Exceptions.Abstractions;
using BloggerSample.Application.Common.Exceptions.Blogs;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Domain.Entities;

namespace BloggerSample.Application.Blogs.Commands.Edit;

public sealed class EditBlogService : IEditBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditBlogService(
        IBlogRepository blogRepository,
        IUnitOfWork unitOfWork)
    {
        _blogRepository = blogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Execute(
        EditBlogDto editBlogDto,
        Guid id,
        CancellationToken cancellationToken)
    {
        await GuardAgainstDuplicateTitle(editBlogDto, id, cancellationToken);

        var blog = await _blogRepository.FindById(id, cancellationToken);

        if (blog is null)
            throw new NotFoundException(nameof(Blog), id);

        blog.Title = editBlogDto.title;
        blog.Body = editBlogDto.body;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task GuardAgainstDuplicateTitle(
        EditBlogDto editBlogDto,
        Guid id,
        CancellationToken cancellationToken)
    {
        if (await _blogRepository.IsTitleDuplicate(editBlogDto.title, id, cancellationToken))
            throw new DuplicateTitleException(editBlogDto.title);
    }
}
