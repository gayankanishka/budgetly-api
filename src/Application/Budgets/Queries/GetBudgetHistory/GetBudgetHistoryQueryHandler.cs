using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgetHistory;

public class GetBudgetHistoryQueryHandler : IRequestHandler<GetBudgetHistoryQuery, IEnumerable<BudgetHistoryDto>>
{
    private readonly IBudgetItemRepository _repository;

    public GetBudgetHistoryQueryHandler(IBudgetItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BudgetHistoryDto>> Handle(GetBudgetHistoryQuery request, CancellationToken cancellationToken)
    {
        // TODO: remove below once implemented properly
        
        return new List<BudgetHistoryDto>
        {
            new()
            {
                TargetExpense = 150000,
                ActualExpense = 130000,
                ActualIncome = 250000,
                Date = new DateTime(2021, 9, 1)
            },
            new()
            {
                TargetExpense = 150000,
                ActualExpense = 180000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 10, 1)
            },
            new()
            {
                TargetExpense = 170000,
                ActualExpense = 20000,
                ActualIncome = 340000,
                Date = new DateTime(2021, 11, 1)
            },
            new()
            {
                TargetExpense = 120000,
                ActualExpense = 200000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 12, 1)
            },
            new()
            {
                TargetExpense = 120000,
                ActualExpense = 50000,
                ActualIncome = 100000,
                Date = new DateTime(2022, 01, 1)
            }
        };
    }
}