namespace Budgetly.Domain.Entities;

public class BudgetItem : BaseEntity
{
    public double TargetExpense { get; set; }
    public double ActualExpense { get; set; }
    public string? Description { get; set; }
    public int TransactionCategoryId { get; set; }
    public TransactionCategory TransactionCategory { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}