using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreatTransactionCategoryCommandHandler : IRequestHandler<CreatTransactionCategoryCommand,
    TransactionCategoryDto>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreatTransactionCategoryCommandHandler(ITransactionCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionCategoryDto> Handle(CreatTransactionCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var transactionCategory = _mapper.Map<TransactionCategory>(request);
        
        await _repository.AddAsync(transactionCategory, cancellationToken);
        return _mapper.Map<TransactionCategoryDto>(transactionCategory);
    }
}
