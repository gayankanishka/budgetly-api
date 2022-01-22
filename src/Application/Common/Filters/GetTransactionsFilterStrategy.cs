using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filters;

public class GetTransactionsFilterStrategy : IFilterStrategy
{
    public object Filter(object query, object filters)
    {
        var transactionQuery = query as IQueryable<Transaction>;

        if (transactionQuery != null && !transactionQuery.Any()
            || filters is not GetTransactionsQuery transactionFilters)
        {
            return query;
        }

        transactionFilters.StartDate ??= new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1);

        transactionFilters.EndDate ??= new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month,
            DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month));

        if (!string.IsNullOrWhiteSpace(transactionFilters?.Name))
        {
            transactionQuery = transactionQuery?.Where(x => x.Name.Contains(transactionFilters.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        }

        if (transactionFilters?.Recurring != null)
        {
            transactionQuery = transactionQuery?.Where(x => x.IsRecurring == transactionFilters.Recurring);
        }

        if (transactionFilters?.TransactionType != null)
        {
            transactionQuery = transactionQuery?.Where(x => x.Type == transactionFilters.TransactionType);
        }

        if (transactionFilters?.CategoryId != null)
        {
            transactionQuery = transactionQuery?.Where(x => x.CategoryId == transactionFilters.CategoryId);
        }

        transactionQuery = transactionQuery?.Where(x =>
            x.DateTime >= transactionFilters.StartDate && x.DateTime <= transactionFilters.EndDate);

        return transactionQuery;
    }
}