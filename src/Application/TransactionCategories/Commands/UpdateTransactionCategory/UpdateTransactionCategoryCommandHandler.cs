using AutoMapper;
using Budgetly.Application.Common.Interfaces;
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
        var transactionCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (transactionCategory == null)
        {
            throw new Exception($"Transaction category with id {request.Id} not found");
        }
        
        transactionCategory.Name = request.Name;
        transactionCategory.Description = request.Description;
        transactionCategory.IsPreset = request.IsPreset;
        
        await _repository.UpdateAsync(transactionCategory, cancellationToken);
        return Unit.Value;
    }
}