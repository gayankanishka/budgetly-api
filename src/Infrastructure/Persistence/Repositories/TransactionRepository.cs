using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
    }
}