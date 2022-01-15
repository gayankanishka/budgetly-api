using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;

public class UpdateTransactionCategoryCommandHandler : IRequestHandler<UpdateTransactionCategoryCommand>
{
    private readonly ITransactionCategoryRepository _repository;

    public UpdateTransactionCategoryCommandHandler(ITransactionCategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
    {
        var transactionCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (transactionCategory == null)
        {
            throw new NotFoundException(nameof(TransactionCategory), request.Id);
        }

        transactionCategory.Name = request.Name;
        transactionCategory.Description = request.Description;
        transactionCategory.IsPreset = request.IsPreset;

        await _repository.UpdateAsync(transactionCategory, cancellationToken);
        return Unit.Value;
    }
}