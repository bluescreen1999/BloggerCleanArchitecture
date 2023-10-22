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

        public async Task<bool> IsTitleDuplicate(
            string title,
            CancellationToken cancellationToken)
        {
            return await _blogs.AnyAsync(_ => _.Title == title);
        }
    }
}
