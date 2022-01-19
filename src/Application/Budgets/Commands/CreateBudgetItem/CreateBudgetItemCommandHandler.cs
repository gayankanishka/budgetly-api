using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.CreateBudgetItem;

public class CreateBudgetItemCommandHandler : IRequestHandler<CreateBudgetItemCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _repository;

    public CreateBudgetItemCommandHandler(IMapper mapper, IBudgetItemRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateBudgetItemCommand request,
        CancellationToken cancellationToken)
    {
        var exists =
            await _repository.BudgetForTransactionCategoryExistsAsync(request.TransactionCategoryId, cancellationToken);

        if (exists)
        {
            throw new AlreadyExistsException(
                $"Budget Item already exists with transactionCategoryId: {request.TransactionCategoryId}");
        }

        var budgetItem = _mapper.Map<BudgetItem>(request);
        
        budgetItem.DomainEvents.Add(new BudgetItemCreatedEvent(budgetItem));
        await _repository.AddAsync(budgetItem, cancellationToken);

        return budgetItem.Id;
    }
}