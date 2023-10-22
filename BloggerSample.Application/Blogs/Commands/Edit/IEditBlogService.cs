namespace BloggerSample.Application.Blogs.Commands.Edit
{
    public interface IEditBlogService
    {
        Task<bool> Execute(EditBlogDto editBlogDto, Guid id, CancellationToken cancellationToken);
    }
}
