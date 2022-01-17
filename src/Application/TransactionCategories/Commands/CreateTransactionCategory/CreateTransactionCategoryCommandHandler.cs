using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreateTransactionCategoryCommandHandler : IRequestHandler<CreateTransactionCategoryCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;

    public CreateTransactionCategoryCommandHandler(IMapper mapper, ITransactionCategoryRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateTransactionCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.TransactionCategoryExistsWithNameAsync(request.Name, cancellationToken);

        if (exists)
        {
            throw new AlreadyExistsException($"Transaction Category already exists with name: {request.Name}");
        }

        var transactionCategory = _mapper.Map<TransactionCategory>(request);
        await _repository.AddAsync(transactionCategory, cancellationToken);

        return transactionCategory.Id;
    }
}