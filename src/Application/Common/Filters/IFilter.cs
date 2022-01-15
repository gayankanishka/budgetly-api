namespace Budgetly.Application.Common.Filters;

public interface IFilter
{
    public string Name { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }
}