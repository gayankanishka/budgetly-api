using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionCategoryRepository : ITransactionCategoryRepository
{
    private readonly IApplicationDbContext _context;
    
    public TransactionCategoryRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public virtual IQueryable<TransactionCategory> GetAll()
    {
        return _context.TransactionCategories;
    }

    public virtual async Task<TransactionCategory?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TransactionCategories
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual async Task AddAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        await _context.TransactionCategories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        _context.TransactionCategories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        _context.TransactionCategories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}