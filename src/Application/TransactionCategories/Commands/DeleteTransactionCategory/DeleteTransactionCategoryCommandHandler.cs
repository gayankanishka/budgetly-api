using Budgetly.Application.Common.Interfaces;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;

public class DeleteTransactionCategoryCommandHandler : IRequestHandler<DeleteTransactionCategoryCommand>
{
    private readonly ITransactionCategoryRepository _repository;

    public DeleteTransactionCategoryCommandHandler(ITransactionCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
    {
        var transactionCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (transactionCategory == null)
        {
            throw new Exception($"Transaction category with id {request.Id} not found");
        }
        
        await _repository.DeleteAsync(transactionCategory, cancellationToken);
        return Unit.Value;
    }
}
