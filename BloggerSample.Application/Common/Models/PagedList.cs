namespace BloggerSample.Application.Common.Models
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public interface IPaginationService<T>
    {
        Task<PagedList<T>> Paginate(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }

    public class PagedList<T>
    {
        public PagedList(
            int totalItems,
            IReadOnlyCollection<T> items,
            int pageNumber,
            int pageSize)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public IReadOnlyCollection<T> Items { get; }
        public int TotalPages =>
              (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber =>
               HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber =>
               HasPreviousPage ? PageNumber - 1 : 1;
    }
}
