using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Budgetly.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal sealed class TransactionRepository : ITransactionRepository
{
    private readonly IApplicationDbContext _context;
    private IFilterStrategy _filterStrategy;

    public TransactionRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _filterStrategy = new GetTransactionsFilterStrategy();
    }
    
    public void SetFilterStrategy(IFilterStrategy filterStrategy)
    {
        _filterStrategy = filterStrategy ?? throw new ArgumentNullException(nameof(filterStrategy));
    }

    public IQueryable<Transaction> GetAll()
    {
        return _context.Transactions;
    }
    
    public IQueryable<Transaction> GetAll(IFilter filter)
    { 
        return _filterStrategy.Filter(GetAll(), filter) as IQueryable<Transaction> 
               ?? throw new InvalidOperationException();
    }

    public async Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task AddAsync(Transaction entity, CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Transaction entity, CancellationToken cancellationToken)
    {
        _context.Transactions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Transaction entity, CancellationToken cancellationToken)
    {
        _context.Transactions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<double> GetActualIncomeAsync(DateTimeOffset startDate, DateTimeOffset endDate,
        CancellationToken cancellationToken)
    {
        return await GetAll()
            .Where(x => x.Type == TransactionTypes.Income)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .AsNoTracking()
            .Select(x => x.Amount)
            .SumAsync(cancellationToken);
    }

    public async Task<double> GetActualExpenseAsync(DateTimeOffset startDate, DateTimeOffset endDate,
        CancellationToken cancellationToken)
    {
        return await GetAll()
            .Where(x => x.Type == TransactionTypes.Expense)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .AsNoTracking()
            .Select(x => x.Amount)
            .SumAsync(cancellationToken);
    }
}