using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Enums;
using Budgetly.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionUpdatedEventHandler : INotificationHandler<DomainEventNotification<TransactionUpdatedEvent>>
{
    private readonly ILogger<TransactionUpdatedEventHandler> _logger;
    private readonly IBudgetItemRepository _repository;

    public TransactionUpdatedEventHandler(ILogger<TransactionUpdatedEventHandler> logger,
        IBudgetItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(DomainEventNotification<TransactionUpdatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var transaction = domainEvent.Transaction;
        
        if (transaction.Type == TransactionTypes.Income)
        {
            return;
        }

        var budgetItem = await _repository.GetAll()
            .Include(x => x.Transactions)
            .Where(x => x.TransactionCategoryId == transaction.CategoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (budgetItem == null)
        {
            return;
        }

        budgetItem.ActualExpense = budgetItem.Transactions.Sum(x => x.Amount);
        await _repository.UpdateAsync(budgetItem, cancellationToken);

        _logger.LogInformation("----- Budgetly API Domain Event: {DomainEvent} updated budget item" +
                               " {BudgetItemId}", domainEvent.GetType().Name, budgetItem.Id);
    }
}