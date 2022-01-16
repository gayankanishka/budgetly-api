using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionCreatedEventHandler : INotificationHandler<DomainEventNotification<TransactionCreatedEvent>>
{
    private readonly IBudgetItemRepository _repository;

    public TransactionCreatedEventHandler(IBudgetItemRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DomainEventNotification<TransactionCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}