using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;

public class DeleteTransactionCategoryCommand : IRequest
{
    public int Id { get; private set; }

    public DeleteTransactionCategoryCommand(int id)
    {
        Id = id;
    }
}