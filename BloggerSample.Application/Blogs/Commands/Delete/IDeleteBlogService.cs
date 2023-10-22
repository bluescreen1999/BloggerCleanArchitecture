namespace BloggerSample.Application.Blogs.Commands.Delete
{
    public interface IDeleteBlogService
    {
        Task Execute(Guid id, CancellationToken cancellationToken);
    }
}
