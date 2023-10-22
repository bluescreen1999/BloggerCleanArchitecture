using BloggerSample.Application.Common.Models;

namespace BloggerSample.Application.Blogs.Queries.GetAll
{
    public sealed class GetAllBlogsDto
    {
        public GetAllBlogsDto()
        {
            OperationLinks = new List<OperationLink>();
        }

        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required bool IsDeleted { get; init; }
        public required string? Body { get; set; }
        public required DateTimeOffset CreationDateTime { get; init; }
        public List<OperationLink> OperationLinks { get; set; }
    }
}
