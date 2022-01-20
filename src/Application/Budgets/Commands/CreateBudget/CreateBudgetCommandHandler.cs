using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.CreateBudget;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IBudgetRepository _repository;

    public CreateBudgetCommandHandler(IMapper mapper, IBudgetRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateBudgetCommand request,
        CancellationToken cancellationToken)
    {
        var exists =
            await _repository.BudgetForTransactionCategoryExistsAsync(request.TransactionCategoryId, cancellationToken);

        if (exists)
        {
            throw new AlreadyExistsException(
                $"Budget item already exists with the selected transaction category.");
        }
        
        var budget = _mapper.Map<Budget>(request);
        
        budget.DomainEvents.Add(new BudgetCreatedEvent(budget));
        await _repository.AddAsync(budget, cancellationToken);

        return budget.Id;
    }
}