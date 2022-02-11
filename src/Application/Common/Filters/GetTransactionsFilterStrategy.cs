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
        
        if (!string.IsNullOrWhiteSpace(transactionFilters?.Name))
        {
            transactionQuery = transactionQuery?.Where(x =>
                x.Name.ToLower().Contains(transactionFilters.Name.Trim().ToLower()));
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

        if (transactionFilters?.StartDate != null && transactionFilters?.EndDate != null)
        {
            return transactionQuery?.Where(x =>
                x.DateTime >= transactionFilters.StartDate && x.DateTime <= transactionFilters.EndDate);
        }
        
        if (transactionFilters?.StartDate != null && transactionFilters?.EndDate == null)
        {
            return transactionQuery?.Where(x => x.DateTime >= transactionFilters.StartDate);
        }

        if (transactionFilters?.StartDate == null && transactionFilters?.EndDate != null)
        {
            return transactionQuery?.Where(x => x.DateTime <= transactionFilters.EndDate);
        }
        
        return transactionQuery;
    }
}