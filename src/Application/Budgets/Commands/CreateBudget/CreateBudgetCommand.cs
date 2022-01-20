using MediatR;

namespace Budgetly.Application.Budgets.Commands.CreateBudget;

public class CreateBudgetCommand : IRequest<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TransactionCategoryId { get; set; }
    public double TargetExpense { get; set; }
}