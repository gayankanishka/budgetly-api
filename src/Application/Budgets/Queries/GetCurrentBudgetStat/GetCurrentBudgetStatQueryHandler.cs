using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;

public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IBudgetItemRepository _budgetItemRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeService _dateTimeService;

    public GetCurrentBudgetStatQueryHandler(IBudgetItemRepository itemRepository,
        ITransactionRepository transactionRepository,
        IDateTimeService dateTimeService)
    {
        _budgetItemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _transactionRepository =
            transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {
        var startDate = _dateTimeService.FirstDayOfCurrentMonth;
        var endDate = _dateTimeService.LastDayOfCurrentMonth;

        var targetExpense = await _budgetItemRepository.GetTargetExpenseAsync(cancellationToken);
        var actualIncome = await _transactionRepository.GetActualIncomeAsync(startDate, endDate, cancellationToken);
        var actualExpense = await _transactionRepository.GetActualExpenseAsync(startDate, endDate, cancellationToken);

        return new BudgetStatDto()
        {
            TargetExpense = targetExpense,
            ActualExpense = actualExpense,
            AvailableToSpend = actualIncome - actualExpense
        };
    }
}