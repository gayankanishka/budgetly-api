using Budgetly.Application.Common.Filterings;

namespace Budgetly.Application.Parameters;

public class QueryParameters : PaginationParameters, IFilters
{
    // public virtual string OrderBy { get; set; }
    // public virtual string Fields { get; set; }
    public string? Name { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}