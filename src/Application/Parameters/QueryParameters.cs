using Budgetly.Application.Common.Filters;

namespace Budgetly.Application.Parameters;

public class QueryParameters : PaginationParameters, IFilter
{
    public string? Name { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}