using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Enums;
using Budgetly.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetly.Application.Budgets.EventHandlers;

public class BudgetCreatedEventHandler : INotificationHandler<DomainEventNotification<BudgetCreatedEvent>>
{
    private readonly ILogger<BudgetCreatedEventHandler> _logger;
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeService _dateTimeService;

    public BudgetCreatedEventHandler(ILogger<BudgetCreatedEventHandler> logger,
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository,
        IDateTimeService dateTimeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _budgetRepository = budgetRepository ?? throw new ArgumentNullException(nameof(budgetRepository));
        _transactionRepository =
            transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public async Task Handle(DomainEventNotification<BudgetCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var budget = domainEvent.Budget;

        var transactions = await _transactionRepository
            .GetAll()
            .Where(x => x.DateTime >= _dateTimeService.FirstDayOfCurrentMonth
                        && x.DateTime <= _dateTimeService.LastDayOfCurrentMonth)
            .Where(x => x.CategoryId == budget.TransactionCategoryId)
            .Where(x => x.Type == TransactionTypes.Expense)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        budget.Transactions = transactions;
        budget.ActualExpense = transactions.Sum(x => x.Amount);

        await _budgetRepository.UpdateAsync(budget, cancellationToken);

        _logger.LogInformation(
            "----- Budgetly API Domain Event: {DomainEvent} associated the relevant transactions by category.",
            domainEvent.GetType().Name);
    }
}