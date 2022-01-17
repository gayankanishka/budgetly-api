using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;

    public CreateTransactionCommandHandler(IMapper mapper, ITransactionRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(request);

        transaction.DomainEvents.Add(new TransactionCreatedEvent(transaction));
        await _repository.AddAsync(transaction, cancellationToken);

        return transaction.Id;
    }
}