using Budgetly.Domain.Entities;

namespace Budgetly.Domain.Dtos;

public class BudgetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double BudgeLimit { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ICollection<BudgetItemDto> BudgetItems { get; set; }
}