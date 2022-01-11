using Budgetly.Domain.Common;

namespace Budgetly.Domain.Entities;

public class TransactionCategory : AuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPreset { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; }
}