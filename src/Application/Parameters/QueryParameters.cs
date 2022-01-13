using Budgetly.Application.Common.Filterings;

namespace Budgetly.Application.Parameters;

public class QueryParameters : PaginationParameters, IFilters
{
    public string? Name { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}