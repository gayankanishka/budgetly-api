using MediatR;

namespace Budgetly.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand : IRequest
{
    public int Id { get; set; }
}