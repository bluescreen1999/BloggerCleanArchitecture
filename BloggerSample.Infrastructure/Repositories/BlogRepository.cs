using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggerSample.Infrastructure.Repositories
{
    public sealed class BlogRepository : IBlogRepository
    {
        private readonly DbSet<Blog> _blogs;

        public BlogRepository(ApplicationDbContext context)
        {
            _blogs = context.Set<Blog>();
        }

        public void Add(Blog blog)
        {
            _blogs.Add(blog);
        }

        public async Task<GetBlogDetailsDto?> GetDetails(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _blogs.Where(_ => _.Id == id)
                .Select(_ => new GetBlogDetailsDto()
                {
                    Title = _.Title,
                    Body = _.Body,
                    IsDeleted = _.IsDeleted,
                    CreationDateTime = _.CreationDateTime
                }).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsTitleDuplicate(
            string title,
            CancellationToken cancellationToken)
        {
            return await _blogs.AnyAsync(_ => _.Title == title && !_.IsDeleted,
                cancellationToken);
        }
    }
}
