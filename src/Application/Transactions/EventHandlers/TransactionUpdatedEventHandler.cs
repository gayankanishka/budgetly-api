using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionUpdatedEventHandler : INotificationHandler<DomainEventNotification<TransactionUpdatedEvent>>
{
    public Task Handle(DomainEventNotification<TransactionUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}