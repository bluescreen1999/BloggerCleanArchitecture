namespace BloggerSample.Application.Blogs.Queries.GetDetails
{
    public record GetBlogDetailsDto
    {
        public required string Title { get; init; }
        public required string Body { get; init; }
        public required bool IsDeleted { get; init; }
        public required DateTimeOffset CreationDateTime { get; init; }

    }
}
