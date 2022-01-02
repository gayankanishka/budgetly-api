using Budgetly.Domain.Common;

namespace Budgetly.Domain.Entities;

public class BudgetItem : AuditableEntity
{
    public int Id { get; set; }
    public int BudgetId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Budget Budget { get; set; }
    public virtual TransactionCategory TransactionCategory { get; set; }
}