using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;

public class UpdateTransactionCategoryCommandHandler : IRequestHandler<UpdateTransactionCategoryCommand>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTransactionCategoryCommandHandler(ITransactionCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
    {
        var transactionCategory = _mapper.Map<TransactionCategory>(request);
        await _repository.UpdateAsync(transactionCategory, cancellationToken);
        return Unit.Value;
    }
}