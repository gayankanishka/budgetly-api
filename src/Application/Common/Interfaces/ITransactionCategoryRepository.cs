using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface ITransactionCategoryRepository : IRepository<TransactionCategory>
{
    Task<bool> TransactionCategoryExistsWithNameAsync(string name, CancellationToken cancellationToken);
}