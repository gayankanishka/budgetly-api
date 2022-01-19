using Budgetly.Application.Budgets.Queries.GetBudgetItems;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filters;

public class GetBudgetItemsFilterStrategy : IFilterStrategy
{
    public object Filter(object query, object filters)
    {
        var budgetItemsQuery = query as IQueryable<BudgetItem>;
        
        if (!budgetItemsQuery.Any()
            || filters is not GetBudgetItemsQuery budgetItemsFilters)
        {
            return query;
        }
        
        if (!string.IsNullOrWhiteSpace(budgetItemsFilters?.Name))
        {
            budgetItemsQuery = budgetItemsQuery.Where(x => x.Name.Contains(budgetItemsFilters.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        }

        if (budgetItemsFilters?.TransactionCategoryId != null)
        {
            budgetItemsQuery = budgetItemsQuery.Where(x =>
                x.TransactionCategoryId == budgetItemsFilters.TransactionCategoryId);
        }
        
        if (budgetItemsFilters?.Exceeded != null)
        {
            if (budgetItemsFilters.Exceeded.HasValue && budgetItemsFilters.Exceeded.Value)
            {
                budgetItemsQuery = budgetItemsQuery.Where(x => x.ActualExpense > x.TargetExpense);
            }
            else
            {
                budgetItemsQuery = budgetItemsQuery.Where(x => x.ActualExpense <= x.TargetExpense);
            }
        }

        return budgetItemsQuery;
    }
}