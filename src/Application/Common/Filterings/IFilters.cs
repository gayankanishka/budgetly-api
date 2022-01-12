namespace Budgetly.Application.Common.Filterings;

public interface IFilters
{
    public string Name { get; set; }
    
    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }
}