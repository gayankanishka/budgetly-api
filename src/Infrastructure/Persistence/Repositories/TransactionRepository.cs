using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionRepository : ITransactionRepository
{
    private readonly IApplicationDbContext _context;
    
    public TransactionRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public virtual IQueryable<Transaction> GetAll()
    {
        return _context.Transactions;
    }

    public virtual async Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual async Task AddAsync(Transaction entity, CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(Transaction entity, CancellationToken cancellationToken)
    {
        _context.Transactions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Transaction entity, CancellationToken cancellationToken)
    {
        _context.Transactions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}