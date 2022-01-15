using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.DeleteBudgetItem;

public class DeleteBudgetItemCommandHandler : IRequestHandler<DeleteBudgetItemCommand>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _repository;

    public DeleteBudgetItemCommandHandler(IMapper mapper, IBudgetItemRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(DeleteBudgetItemCommand request, CancellationToken cancellationToken)
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