namespace Budgetly.Domain.Dtos;

public class BudgetItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BudgetId { get; set; }
    public int CategoryId { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
    public TransactionCategoryDto TransactionCategory { get; set; }
}