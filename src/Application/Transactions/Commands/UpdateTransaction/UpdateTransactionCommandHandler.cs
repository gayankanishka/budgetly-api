using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;

    public UpdateTransactionCommandHandler(IMapper mapper, ITransactionRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

        transaction.DomainEvents.Add(new TransactionUpdatedEvent(transaction));

        await _repository.UpdateAsync(transaction, cancellationToken);

        return Unit.Value;
    }
}