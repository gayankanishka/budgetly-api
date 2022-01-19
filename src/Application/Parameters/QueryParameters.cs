using Budgetly.Application.Common.Interfaces;

namespace Budgetly.Application.Parameters;

public class QueryParameters : PaginationParameters, IFilter
{
    public string? Name { get; set; }
}