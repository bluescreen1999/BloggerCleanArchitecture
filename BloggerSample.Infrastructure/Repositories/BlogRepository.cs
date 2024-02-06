using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Common.Persistence;
using BloggerSample.Application.Common.Models;
using BloggerSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggerSample.Infrastructure.Repositories
{
    public sealed class BlogRepository : IBlogRepository
    {
        private readonly DbSet<Blog> _blogs;
        private readonly IPaginationService<GetAllBlogsDto> _paginationService;

        public BlogRepository(ApplicationDbContext context, 
            IPaginationService<GetAllBlogsDto> paginationService)
        {
            _blogs = context.Set<Blog>();
            _paginationService = paginationService;
        }

        public void Add(Blog blog)
        {
            _blogs.Add(blog);
        }

        public async Task<int> Delete(Guid id, CancellationToken cancellationToken)
        {
            var affectedRows = await _blogs
                .Where(_ => _.Id == id && !_.IsDeleted)
                .ExecuteUpdateAsync(_ => _.SetProperty(_ => _.IsDeleted, true),
                cancellationToken);

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
                .SetProperty(_ => _.Title, editBlogDto.title),
                cancellationToken);

            return affectedRows;
        }

        public async Task<PagedList<GetAllBlogsDto>> GetAll(
            PagingParams pagingParams,
            GetAllBlogsFilterDto? filterDto,
            CancellationToken cancellationToken)
        {
            var blogs = _blogs.Select(_ => new GetAllBlogsDto
            {
                Id = _.Id,
                Title = _.Title,
                Body = _.Body,
                CreationDateTime = _.CreationDateTime,
                IsDeleted = _.IsDeleted
            });

            var filteredBlogs = FilterBlogs(blogs, filterDto);

            var pagedBlogs = await _paginationService.Paginate(
                source: filteredBlogs.OrderByDescending(_ => _.CreationDateTime),
                pageNumber: pagingParams.PageNumber,
                pageSize: pagingParams.PageSize,
                cancellationToken: cancellationToken);

            return pagedBlogs;
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

        public async Task<bool> IsTitleDuplicate(
            string title,
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _blogs.AnyAsync(_ => _.Title == title &&
            _.Id != id && !_.IsDeleted, cancellationToken);
        }

        public async Task<Blog?> FindById(Guid id, CancellationToken cancellationToken)
        {
            return await _blogs
                .SingleOrDefaultAsync(_ => _.Id == id && !_.IsDeleted, cancellationToken);
        }

        private IQueryable<GetAllBlogsDto> FilterBlogs(
            IQueryable<GetAllBlogsDto> blogs,
            GetAllBlogsFilterDto? filterDto)
        {
            if (filterDto is not null)
            {
                blogs = FilterByTitle(blogs, filterDto.Title);

                blogs = FilterByBody(blogs, filterDto.Body);

                blogs = FilterByIsDeleted(blogs, filterDto.IsDeleted);
            }

            return blogs;
        }

        private IQueryable<GetAllBlogsDto> FilterByIsDeleted(
            IQueryable<GetAllBlogsDto> blogs,
            bool? isDeleted)
        {
            if (isDeleted is not null)
                blogs = blogs.Where(_ => _.IsDeleted == isDeleted);

            return blogs;
        }

        private IQueryable<GetAllBlogsDto> FilterByBody(
            IQueryable<GetAllBlogsDto> blogs,
            string? body)
        {
            if (!string.IsNullOrEmpty(body))
                blogs = blogs.Where(_ => _.Body.Contains(body));

            return blogs;
        }

        private IQueryable<GetAllBlogsDto> FilterByTitle(
            IQueryable<GetAllBlogsDto> blogs,
            string? title)
        {
            if (!string.IsNullOrEmpty(title))
                blogs = blogs.Where(_ => _.Title.Contains(title));

            return blogs;
        }
    }
}
