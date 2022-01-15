using MediatR;

namespace Budgetly.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand : IRequest
{
    public DeleteTransactionCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}