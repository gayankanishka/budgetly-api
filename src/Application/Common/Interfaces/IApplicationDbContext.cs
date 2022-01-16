using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<BudgetItem> BudgetItems { get; }
    DbSet<TransactionCategory> TransactionCategories { get; }
    DbSet<Transaction> Transactions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}