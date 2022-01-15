using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (transaction == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        transaction.Name = request.Name;
        transaction.Amount = request.Amount;
        transaction.Type = request.Type;
        transaction.DateTime = request.DateTime;
        transaction.Note = request.Note;
        transaction.CategoryId = request.CategoryId;
        transaction.IsRecurring = request.IsRecurring;

        await _repository.UpdateAsync(transaction, cancellationToken);

        return Unit.Value;
    }
}