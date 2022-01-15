using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.CreateBudgetItem;

public class CreateBudgetItemCommand : IRequest<BudgetItemDto>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public double TargetExpense { get; set; }
}