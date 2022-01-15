namespace Budgetly.Domain.Dtos;

public class BudgetItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double TargetExpense { get; set; }
    public double? ActualExpense { get; set; }
    public string? Description { get; set; }
    public TransactionCategoryDto TransactionCategory { get; set; }
}