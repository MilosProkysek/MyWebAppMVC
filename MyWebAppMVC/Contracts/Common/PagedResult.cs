namespace MyWebAppMVC.Contracts.Common;

/// <summary>
/// Wraps a paginated list of items with metadata.
/// </summary>
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; init; }
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResult(IEnumerable<T> items, int totalCount, PaginationQuery query)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = query.PageNumber;
        PageSize = query.PageSize;
    }
}