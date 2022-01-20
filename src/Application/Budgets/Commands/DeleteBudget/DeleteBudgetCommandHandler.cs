using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.DeleteBudget;

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
{
    private readonly IMapper _mapper;
    private readonly IBudgetRepository _repository;

    public DeleteBudgetCommandHandler(IMapper mapper, IBudgetRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (budget == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        await _repository.DeleteAsync(budget, cancellationToken);
        return Unit.Value;
    }
}