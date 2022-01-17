using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;
public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IBudgetItemRepository _itemRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeService _dateTimeService;

    public GetCurrentBudgetStatQueryHandler(IBudgetItemRepository itemRepository, ITransactionRepository transactionRepository,
        IDateTimeService dateTimeService)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {
        var targetExpense = _itemRepository.GetAll()
            .AsNoTracking()
            .Select(x => x.TargetExpense)
            .Sum();

        var startDate = _dateTimeService.FirstDayOfCurrentMonth;

        var endDate = _dateTimeService.LastDayOfCurrentMonth;

        var actualExpense = _transactionRepository.GetAll()
            .Where(x => x.Type == Domain.Enums.TransactionTypes.Expense)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .AsNoTracking()
            .Select(x => x.Amount)
            .Sum();

        var actualIncome = _transactionRepository.GetAll()
            .Where(x => x.Type == Domain.Enums.TransactionTypes.Income)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .AsNoTracking()
            .Select(x => x.Amount)
            .Sum();

        return new BudgetStatDto()
        {
            TargetExpense = targetExpense,
            ActualExpense = actualExpense,
            AvailableToSpend = actualIncome - actualExpense
        };
    }
}