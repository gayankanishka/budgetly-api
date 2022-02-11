using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filters;

public class GetTransactionCategoriesFilterStrategy : IFilterStrategy
{
    public object Filter(object query, object filters)
    {
        var transactionCategoriesQuery = query as IQueryable<TransactionCategory>;

        if (transactionCategoriesQuery != null && !transactionCategoriesQuery.Any()
            || filters is not GetTransactionCategoriesQuery transactionCategoriesFilters)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(transactionCategoriesFilters?.Name))
        {
            transactionCategoriesQuery = transactionCategoriesQuery?.Where(x =>
                x.Name.ToLower().Contains(transactionCategoriesFilters.Name.Trim().ToLower()));
        }

        if (transactionCategoriesFilters?.Preset != null)
        {
            transactionCategoriesQuery = transactionCategoriesQuery?.Where(x =>
                x.IsPreset == transactionCategoriesFilters.Preset);
        }

        return transactionCategoriesQuery;
    }
}