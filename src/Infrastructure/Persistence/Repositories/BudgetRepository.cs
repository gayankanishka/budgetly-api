using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal sealed class BudgetRepository : IBudgetRepository
{
    private readonly IApplicationDbContext _context;
    private IFilterStrategy _filterStrategy;

    public BudgetRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _filterStrategy = new GetBudgetsFilterStrategy();
    }

    public void SetFilterStrategy(IFilterStrategy filterStrategy)
    {
        _filterStrategy = filterStrategy ?? throw new ArgumentNullException(nameof(filterStrategy));
    }

    public IQueryable<Budget> GetAll()
    {
        return _context.Budgets;
    }

    public IQueryable<Budget> GetAll(IFilter filter)
    {
        return _filterStrategy.Filter(GetAll(), filter) as IQueryable<Budget>
               ?? throw new InvalidOperationException();
    }

    public async Task<Budget?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Budgets
            .FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task AddAsync(Budget entity, CancellationToken cancellationToken)
    {
        await _context.Budgets.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Budget entity, CancellationToken cancellationToken)
    {
        _context.Budgets.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Budget entity, CancellationToken cancellationToken)
    {
        _context.Budgets.Remove(entity);
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