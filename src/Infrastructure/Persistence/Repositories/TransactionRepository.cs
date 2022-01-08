using System.Transactions;
using Budgetly.Application.Common.Interfaces;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }
}