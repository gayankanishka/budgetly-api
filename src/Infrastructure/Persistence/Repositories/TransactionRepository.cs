using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(GetTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        IQueryable<Transaction> transactions = GetAll()
            .Include(x => x.Category)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .OrderByDescending(x => x.DateTime);
        
        FilterData(ref transactions, query.Name, query.StartDate, query.EndDate, query.Recurring);
        
        return await transactions.ToListAsync(cancellationToken);
    }

    private void FilterData(ref IQueryable<Transaction> transactions, string? name, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool? recurring)
    {
        if (!transactions.Any())
        {
            return;
        }

        var predicate = PredicateBuilder.New<Transaction>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            predicate = predicate.And(p => p.Name.Contains(name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        }

        if (recurring != null)
        {
            predicate = predicate.And(p => p.IsRecurring == recurring);
        }
        
        predicate = predicate.And(p => p.DateTime >= startDate && p.DateTime <= endDate);

        transactions = transactions.Where(predicate);
    }
}