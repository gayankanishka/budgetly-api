using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;

public class DeleteTransactionCategoryCommand : IRequest
{
    public DeleteTransactionCategoryCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}