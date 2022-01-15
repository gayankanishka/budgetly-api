using Budgetly.Application.Budgets.Queries.GetBudgets;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filters;

public static class FilteringExtensions
{
    public static IQueryable<T> ForCurrentUser<T>(this IQueryable<T> query, string? userId) 
        where T : BaseEntity => query.Where(w => w.UserId == userId);

    public static IQueryable<TEntity> ApplyFilters<TEntity, TQuery>(this IQueryable<TEntity> query, TQuery filters)
        where TEntity : BaseEntity
        where TQuery : IFilter
    {
        if (!query.Any())
        {
            return query;
        }

        filters.StartDate ??= new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1);

        filters.EndDate ??= new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month,
            DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month));
        
        if (!string.IsNullOrWhiteSpace(filters.Name))
        {
            query = query.Where(x => x.Name.Contains(filters.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        }
        
        switch (filters)
        {
            // Add your filter types here
            case GetTransactionsQuery transactionFilters when query is IQueryable<Transaction> transactionQuery:
            {
                return (IQueryable<TEntity>)transactionQuery
                    .FilterTransactions(transactionFilters);
            }
            case GetTransactionCategoriesQuery transactionCategoriesFilters when query is IQueryable<TransactionCategory> transactionCategoriesQuery:
            {
                return (IQueryable<TEntity>)transactionCategoriesQuery
                    .FilterTransactionCategories(transactionCategoriesFilters);
            }
            case GetBudgetsQuery budgetFilters when query is IQueryable<Budget> budgetQuery:
            {
                return (IQueryable<TEntity>)budgetQuery
                    .FilterBudgets(budgetFilters);
            }
            default:
                return query;
        }
    }

    #region Filter Types

    private static IQueryable<Transaction> FilterTransactions(this IQueryable<Transaction> transactionQuery, 
        GetTransactionsQuery transactionFilters)
    {
        if (transactionFilters.Recurring != null)
        {
            transactionQuery = transactionQuery.Where(x => x.IsRecurring == transactionFilters.Recurring);
        }

        if (transactionFilters.TransactionType != null)
        {
            transactionQuery = transactionQuery.Where(x => x.Type == transactionFilters.TransactionType);
        }
                
        if (transactionFilters.CategoryId != null)
        {
            transactionQuery = transactionQuery.Where(x => x.CategoryId == transactionFilters.CategoryId);
        }
            
        transactionQuery = transactionQuery.Where(x => 
            x.DateTime >= transactionFilters.StartDate && x.DateTime <= transactionFilters.EndDate);

        return transactionQuery;
    }

    private static IQueryable<TransactionCategory> FilterTransactionCategories(this IQueryable<TransactionCategory> query,
        GetTransactionCategoriesQuery filters)
    {
        if (filters.Preset != null)
        {
            query = query.Where(x => x.IsPreset == filters.Preset);
        }

        return query;
    }

    private static IQueryable<Budget> FilterBudgets(this IQueryable<Budget> query, GetBudgetsQuery filters)
    {
        // TODO: GK | Add filtering for budgets
                
        query = query.Where(x => 
            x.StartDate >= filters.StartDate && x.EndDate <= filters.EndDate);

        return query;
    }

    #endregion
}