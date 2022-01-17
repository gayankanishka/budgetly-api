using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal sealed class TransactionCategoryRepository : ITransactionCategoryRepository
{
    private readonly IApplicationDbContext _context;
    
    public TransactionCategoryRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IQueryable<TransactionCategory> GetAll()
    {
        return _context.TransactionCategories;
    }

    public async Task<TransactionCategory?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TransactionCategories
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task AddAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        await _context.TransactionCategories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        _context.TransactionCategories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TransactionCategory entity, CancellationToken cancellationToken)
    {
        _context.TransactionCategories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> TransactionCategoryExistsWithNameAsync(string name, CancellationToken cancellationToken)
    {
        return await GetAll()
            .AsNoTracking()
            .AnyAsync(x => x.Name == name, cancellationToken);
    }
}