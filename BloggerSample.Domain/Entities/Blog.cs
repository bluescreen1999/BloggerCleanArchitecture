namespace BloggerSample.Domain.Entities
{
    public sealed class Blog
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required bool IsDeleted { get; set; }
        public required DateTimeOffset CreationDateTime { get; set; }
    }
}
