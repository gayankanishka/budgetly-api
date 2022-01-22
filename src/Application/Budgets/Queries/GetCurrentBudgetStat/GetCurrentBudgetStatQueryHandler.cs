using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;

public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ITransactionRepository _transactionRepository;

    public GetCurrentBudgetStatQueryHandler(IBudgetRepository repository,
        IDateTimeService dateTimeService,
        ITransactionRepository transactionRepository)
    {
        _budgetRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        _transactionRepository =
            transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {
        var startDate = _dateTimeService.FirstDayOfCurrentMonth;
        var endDate = _dateTimeService.LastDayOfCurrentMonth;

        var targetExpense = await _budgetRepository.GetTargetExpenseAsync(cancellationToken);
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