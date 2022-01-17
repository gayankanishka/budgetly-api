namespace Budgetly.Domain.Entities;

public class TransactionCategory : BaseEntity
{
    // TODO: Apply bridge pattern
    public string? Description { get; set; }
    public bool IsPreset { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; }
}