using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class BudgetItemItemRepository : IBudgetItemRepository
{
    private readonly IApplicationDbContext _context;
    
    public BudgetItemItemRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public virtual IQueryable<BudgetItem> GetAll()
    {
        return _context.BudgetItems;
    }

    public virtual async Task<BudgetItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.BudgetItems
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual async Task AddAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        await _context.BudgetItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        _context.BudgetItems.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        _context.BudgetItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}