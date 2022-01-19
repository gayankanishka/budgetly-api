using MediatR;

namespace Budgetly.Application.Budgets.Commands.UpdateBudgetItem;

public class UpdateBudgetItemCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double TargetExpense { get; set; }
}