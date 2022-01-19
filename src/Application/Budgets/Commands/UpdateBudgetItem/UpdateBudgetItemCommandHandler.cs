using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.UpdateBudgetItem;

public class UpdateBudgetItemCommandHandler : IRequestHandler<UpdateBudgetItemCommand>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _repository;

    public UpdateBudgetItemCommandHandler(IMapper mapper, IBudgetItemRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(UpdateBudgetItemCommand request, CancellationToken cancellationToken)
    {
        var budget = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (budget == null)
        {
            throw new NotFoundException(nameof(BudgetItem), request.Id);
        }

        budget.Name = request.Name;
        budget.Description = request.Description;
        budget.TargetExpense = request.TargetExpense;

        await _repository.UpdateAsync(budget, cancellationToken);

        return Unit.Value;
    }
}