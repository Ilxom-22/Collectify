using Collectify.Domain.Pagination;

namespace Collectify.Persistence.Extensions;

public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(
        this IQueryable<TSource> source, PaginationOptions paginationOptions)
    {
        return source
            .Skip((paginationOptions.PageToken - 1) * paginationOptions.PageSize)
            .Take(paginationOptions.PageSize);
    }

    public static IEnumerable<TSource> ApplyPagination<TSource>(
        this IEnumerable<TSource> source, PaginationOptions paginationOptions)
    {
        return source
            .Skip((paginationOptions.PageToken - 1) * paginationOptions.PageSize)
            .Take(paginationOptions.PageSize);
    }
}