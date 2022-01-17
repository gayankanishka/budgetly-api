using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;

public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IBudgetItemRepository _budgetItemRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ITransactionRepository _transactionRepository;

    public GetCurrentBudgetStatQueryHandler(IBudgetItemRepository itemRepository,
        IDateTimeService dateTimeService,
        ITransactionRepository transactionRepository)
    {
        _budgetItemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        _transactionRepository =
            transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {
        var startDate = _dateTimeService.FirstDayOfCurrentMonth;
        var endDate = _dateTimeService.LastDayOfCurrentMonth;

        var targetExpense = await _budgetItemRepository.GetTargetExpenseAsync(cancellationToken);
        var actualIncome = await _transactionRepository.GetActualIncomeAsync(startDate, endDate, cancellationToken);
        var actualExpense = await _transactionRepository.GetActualExpenseAsync(startDate, endDate, cancellationToken);

        return new BudgetStatDto
        {
            TargetExpense = targetExpense,
            ActualExpense = actualExpense,
            AvailableToSpend = actualIncome - actualExpense
        };
    }
}