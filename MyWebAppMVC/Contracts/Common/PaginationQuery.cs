namespace MyWebAppMVC.Contracts.Common;

/// <summary>
/// Common query parameters for paginated API requests.
/// </summary>
public class PaginationQuery
{
    private const int MaxPageSize = 100;
    private int _pageSize = 10;

    /// <summary>Current page number (1-based).</summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>Number of items per page. Capped at 100.</summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    /// <summary>Number of items to skip for EF Core queries.</summary>
    public int Skip => (PageNumber - 1) * PageSize;
}