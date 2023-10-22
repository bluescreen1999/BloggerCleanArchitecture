using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Commands.Edit;
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

        public async Task<int> Delete(Guid id, CancellationToken cancellationToken)
        {
            var affectedRows = await _blogs
                .Where(_ => _.Id == id && !_.IsDeleted)
                .ExecuteUpdateAsync(_ => _.SetProperty(_ => _.IsDeleted, true), cancellationToken);

            return affectedRows;
        }

        public async Task<int> Edit(
            Guid id,
            CancellationToken cancellationToken,
            EditBlogDto editBlogDto)
        {
            var affectedRows = await _blogs
                .Where(_ => _.Id == id && !_.IsDeleted)
                .ExecuteUpdateAsync(_ => 
                _.SetProperty(_ => _.Body, editBlogDto.body)
                .SetProperty(_ => _.Title, editBlogDto.title), cancellationToken);

            return affectedRows;
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
