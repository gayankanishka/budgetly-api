using MediatR;

namespace Budgetly.Application.Budgets.Commands.DeleteBudgetItem;

public class DeleteBudgetItemCommand : IRequest
{
    public DeleteBudgetItemCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}