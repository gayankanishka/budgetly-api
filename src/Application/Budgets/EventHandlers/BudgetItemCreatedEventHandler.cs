using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetly.Application.Budgets.EventHandlers;

public class BudgetItemCreatedEventHandler : INotificationHandler<DomainEventNotification<BudgetItemCreatedEvent>>
{
    private readonly ILogger<BudgetItemCreatedEventHandler> _logger;
    private readonly IBudgetItemRepository _budgetItemRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeService _dateTimeService;

    public BudgetItemCreatedEventHandler(ILogger<BudgetItemCreatedEventHandler> logger,
        IBudgetItemRepository budgetItemRepository,
        ITransactionRepository transactionRepository,
        IDateTimeService dateTimeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _budgetItemRepository = budgetItemRepository ?? throw new ArgumentNullException(nameof(budgetItemRepository));
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public async Task Handle(DomainEventNotification<BudgetItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var budgetItem = domainEvent.BudgetItem;

        var transaction = await _transactionRepository
            .GetAll()
            .Where(x => x.DateTime >= _dateTimeService.FirstDayOfCurrentMonth
                        && x.DateTime <= _dateTimeService.LastDayOfCurrentMonth)
            .Where(x => x.CategoryId == budgetItem.TransactionCategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        budgetItem.Transactions = transaction;
        budgetItem.ActualExpense = transaction.Sum(x => x.Amount);

        await _budgetItemRepository.UpdateAsync(budgetItem, cancellationToken);

        _logger.LogInformation("----- Budgetly API Domain Event: {DomainEvent} associated the relevant transactions by category.",
            domainEvent.GetType().Name);
    }
}