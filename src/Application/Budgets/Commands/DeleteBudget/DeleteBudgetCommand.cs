using MediatR;

namespace Budgetly.Application.Budgets.Commands.DeleteBudget;

public class DeleteBudgetCommand : IRequest
{
    public DeleteBudgetCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}