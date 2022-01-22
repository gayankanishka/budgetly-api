using Budgetly.Application.Budgets.Queries.GetBudgets;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filters;

public class GetBudgetsFilterStrategy : IFilterStrategy
{
    public object Filter(object query, object filters)
    {
        var budgetsQuery = query as IQueryable<Budget>;

        if (!budgetsQuery.Any()
            || filters is not GetBudgetsQuery budgetsFilters)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(budgetsFilters?.Name))
        {
            budgetsQuery = budgetsQuery.Where(x => x.Name.Contains(budgetsFilters.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        }

        if (budgetsFilters?.TransactionCategoryId != null)
        {
            budgetsQuery = budgetsQuery.Where(x =>
                x.TransactionCategoryId == budgetsFilters.TransactionCategoryId);
        }

        if (budgetsFilters?.Exceeded != null)
        {
            if (budgetsFilters.Exceeded.HasValue && budgetsFilters.Exceeded.Value)
            {
                budgetsQuery = budgetsQuery.Where(x => x.ActualExpense > x.TargetExpense);
            }
            else
            {
                budgetsQuery = budgetsQuery.Where(x => x.ActualExpense <= x.TargetExpense);
            }
        }

        return budgetsQuery;
    }
}