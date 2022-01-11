using Budgetly.Application.Common.Models;

namespace Budgetly.Application.Mappings;

public static class MappingExtensions
{
    public static Task<PagedResponse<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken)
        where TDestination : class => PagedResponse<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
}