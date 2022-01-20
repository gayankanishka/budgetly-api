using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.UpdateBudget;

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand>
{
    private readonly IMapper _mapper;
    private readonly IBudgetRepository _repository;

    public UpdateBudgetCommandHandler(IMapper mapper, IBudgetRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (budget == null)
        {
            throw new NotFoundException(nameof(Budget), request.Id);
        }

        budget.Name = request.Name;
        budget.Description = request.Description;
        budget.TargetExpense = request.TargetExpense;

        await _repository.UpdateAsync(budget, cancellationToken);

        return Unit.Value;
    }
}