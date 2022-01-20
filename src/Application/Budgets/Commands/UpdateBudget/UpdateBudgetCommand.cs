using MediatR;

namespace Budgetly.Application.Budgets.Commands.UpdateBudget;

public class UpdateBudgetCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double TargetExpense { get; set; }
}