using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreatTransactionCategoryCommandHandler : IRequestHandler<CreatTransactionCategoryCommand,
    TransactionCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;

    public CreatTransactionCategoryCommandHandler(IMapper mapper, ITransactionCategoryRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TransactionCategoryDto> Handle(CreatTransactionCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var transactionCategory = _mapper.Map<TransactionCategory>(request);

        await _repository.AddAsync(transactionCategory, cancellationToken);
        return _mapper.Map<TransactionCategoryDto>(transactionCategory);
    }
}