using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface IBudgetItemRepository : IRepository<BudgetItem>
{
    Task<double> GetTargetExpenseAsync(CancellationToken cancellationToken);
    Task<bool> BudgetForTransactionCategoryExistsAsync(int transactionCategoryId, CancellationToken cancellationToken);
}