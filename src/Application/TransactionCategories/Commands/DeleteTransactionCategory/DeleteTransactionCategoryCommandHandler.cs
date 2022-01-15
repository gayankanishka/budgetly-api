using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;

public class DeleteTransactionCategoryCommandHandler : IRequestHandler<DeleteTransactionCategoryCommand>
{
    private readonly ITransactionCategoryRepository _repository;

    public DeleteTransactionCategoryCommandHandler(ITransactionCategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
    {
        var transactionCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (transactionCategory == null)
        {
            throw new NotFoundException(nameof(TransactionCategory), request.Id);
        }

        await _repository.DeleteAsync(transactionCategory, cancellationToken);
        return Unit.Value;
    }
}