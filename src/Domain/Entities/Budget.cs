using Budgetly.Domain.Common;

namespace Budgetly.Domain.Entities;

public class Budget : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double BudgeLimit { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string UserId { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; }
}