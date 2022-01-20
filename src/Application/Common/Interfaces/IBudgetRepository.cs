using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<double> GetTargetExpenseAsync(CancellationToken cancellationToken);
    Task<bool> BudgetForTransactionCategoryExistsAsync(int transactionCategoryId, CancellationToken cancellationToken);
}