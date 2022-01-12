namespace Budgetly.Domain.Entities;

public class Budget : BaseEntity
{
    public string? Description { get; set; }
    public double BudgeLimit { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; }
}