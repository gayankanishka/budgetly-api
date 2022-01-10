using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetTransactionsAsync(GetTransactionsQuery query, CancellationToken cancellationToken);
    Task<int> GetResultsCountAsync(GetTransactionsQuery query, CancellationToken cancellationToken);
}