using BloggerSample.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggerSample.Infrastructure.Services
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public async Task<PagedList<T>> Paginate(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var totalItems = await source.CountAsync();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(totalItems, items, pageNumber, pageSize);
        }
    }
}
