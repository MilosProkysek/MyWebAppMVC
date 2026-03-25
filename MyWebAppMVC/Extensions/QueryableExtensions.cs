using MyWebAppMVC.Contracts.Common;

namespace MyWebAppMVC.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Applies Skip and Take to an <see cref="IQueryable{T}"/> based on <see cref="PaginationQuery"/>.
    /// </summary>
    public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> query,
        PaginationQuery pagination)
        => query
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);

    /// <summary>
    /// Executes the query and wraps the result in a <see cref="PagedResult{T}"/>.
    /// Hits the database twice: once for total count, once for the page data.
    /// </summary>
    public static PagedResult<T> ToPagedResult<T>(
        this IQueryable<T> query,
        PaginationQuery pagination)
    {
        var totalCount = query.Count();

        var items = query
            .ApplyPagination(pagination)
            .ToList();

        return new PagedResult<T>(items, totalCount, pagination);
    }
}