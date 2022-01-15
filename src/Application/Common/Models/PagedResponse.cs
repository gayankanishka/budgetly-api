using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Common.Models;

public class PagedResponse<T> : Response<T> where T : class
{
    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalResults) : base(data)
    {
        PageNumber = pageNumber >= 1 ? pageNumber : 1;
        PageSize = pageSize >= 1 ? pageSize : 10;
        TotalPages = (int)Math.Ceiling(totalResults / (double)PageSize);
        TotalResults = totalResults;
    }

    public int? PageNumber { get; }
    public int? TotalPages { get; }
    public int? TotalResults { get; }
    public int? PageSize { get; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public static async Task<PagedResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);

        var items = await source.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>(items, pageNumber, pageSize, count);
    }
}