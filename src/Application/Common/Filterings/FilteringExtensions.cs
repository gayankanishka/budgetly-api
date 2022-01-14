using Budgetly.Application.Budgets.Queries.GetBudgets;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filterings;

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
        
        // TODO: GK | We might have to refactor this for better readability
        switch (filters)
        {
            // Add your query types here to chain the query
            case GetTransactionsQuery transactionFilters when query is IQueryable<Transaction> transactionQuery:
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
                    x.DateTime >= filters.StartDate && x.DateTime <= filters.EndDate);

                return (IQueryable<TEntity>)transactionQuery;
            }
            case GetTransactionCategoriesQuery transactionCategoriesFilters when query is IQueryable<TransactionCategory> transactionCategoriesQuery:
            {
                if (transactionCategoriesFilters.Preset != null)
                {
                    transactionCategoriesQuery =
                        transactionCategoriesQuery.Where(x => x.IsPreset == transactionCategoriesFilters.Preset);
                }
                
                return (IQueryable<TEntity>)transactionCategoriesQuery;
            }
            case GetBudgetsQuery budgetFilters when query is IQueryable<Budget> budgetQuery:
            {
                // TODO: GK | Add filtering for budgets
                
                budgetQuery = budgetQuery.Where(x => 
                    x.StartDate >= budgetFilters.StartDate && x.EndDate <= budgetFilters.EndDate);
                
                return (IQueryable<TEntity>)budgetQuery;
            }
            default:
                return query;
        }
    }
}