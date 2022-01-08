using Budgetly.Domain.Common;

namespace Budgetly.Domain.Entities;

public class BudgetItem : AuditableEntity
{
    public int Id { get; set; }
    public int BudgetId { get; set; }
    public int CategoryId { get; set; } // TODO: lets remove this if it's hard to implement
    public double Amount { get; set; }
    public string? Description { get; set; }
    public Budget Budget { get; set; }
    public TransactionCategory TransactionCategory { get; set; }
}