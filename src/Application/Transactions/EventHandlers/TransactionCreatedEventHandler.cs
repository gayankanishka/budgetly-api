using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionCreatedEventHandler : INotificationHandler<DomainEventNotification<TransactionCreatedEvent>>
{
    private readonly ILogger<TransactionCreatedEventHandler> _logger;
    private readonly IBudgetItemRepository _repository;

    public TransactionCreatedEventHandler(ILogger<TransactionCreatedEventHandler> logger,
        IBudgetItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(DomainEventNotification<TransactionCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var transaction = domainEvent.Transaction;

        var budgetItem = await _repository.GetAll()
            .Include(x => x.Transactions)
            .Where(x => x.TransactionCategoryId == transaction.CategoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (budgetItem == null)
        {
            return;
        }

        budgetItem.Transactions.Add(transaction);
        budgetItem.ActualExpense = budgetItem.Transactions.Sum(x => x.Amount);

        await _repository.UpdateAsync(budgetItem, cancellationToken);

        _logger.LogInformation("----- Budgetly API Domain Event: {DomainEvent} updated budget item" +
                               " {BudgetItemId}", domainEvent.GetType().Name, budgetItem.Id);
    }
}