using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal sealed class BudgetItemItemRepository : IBudgetItemRepository
{
    private readonly IApplicationDbContext _context;

    public BudgetItemItemRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<BudgetItem> GetAll()
    {
        return _context.BudgetItems;
    }

    public async Task<BudgetItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.BudgetItems
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task AddAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        await _context.BudgetItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        _context.BudgetItems.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BudgetItem entity, CancellationToken cancellationToken)
    {
        _context.BudgetItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<double> GetTargetExpenseAsync(CancellationToken cancellationToken)
    {
        return await GetAll()
            .AsNoTracking()
            .Select(x => x.TargetExpense)
            .SumAsync(cancellationToken);
    }

    public async Task<bool> BudgetForTransactionCategoryExistsAsync(int transactionCategoryId,
        CancellationToken cancellationToken)
    {
        return await GetAll()
            .AsNoTracking()
            .AnyAsync(x => x.TransactionCategoryId == transactionCategoryId, cancellationToken);
    }
}