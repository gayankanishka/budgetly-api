using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;
public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IBudgetItemRepository _itemRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetCurrentBudgetStatQueryHandler(IBudgetItemRepository itemRepository, ITransactionRepository transactionRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {

        var targetExpense = _itemRepository.GetAll()
            .AsNoTracking()
            .Select(x => x.TargetExpense)
            .Sum();

        var startDate = new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1);

        var endDate = new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month,
            DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month));

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