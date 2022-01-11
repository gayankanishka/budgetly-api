using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    IQueryable<Transaction> GetTransactionsAsync(GetTransactionsQuery query);
}