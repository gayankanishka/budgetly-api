using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetly.Application.Transactions.EventHandlers;

public class TransactionUpdatedEventHandler : INotificationHandler<DomainEventNotification<TransactionUpdatedEvent>>
{
    private readonly ILogger<TransactionUpdatedEventHandler> _logger;
    private readonly IBudgetItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public TransactionUpdatedEventHandler(ILogger<TransactionUpdatedEventHandler> logger, IBudgetItemRepository repository, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DomainEventNotification<TransactionUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var transaction = domainEvent.Transaction;
        
        var budgetItem = await _repository.GetAll()
            .Include(x => x.Transactions)
            .ForCurrentUser(_currentUserService.UserId)
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