using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionDeletedEventHandler : INotificationHandler<DomainEventNotification<TransactionDeletedEvent>>
{
    public Task Handle(DomainEventNotification<TransactionDeletedEvent> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}