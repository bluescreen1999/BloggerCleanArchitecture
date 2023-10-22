namespace BloggerSample.Application.Blogs.Commands.Add
{
    public interface IAddBlogService
    {
        Task<Guid> Execute(AddBlogDto addBlogDto, CancellationToken cancellationToken);
    }
}
