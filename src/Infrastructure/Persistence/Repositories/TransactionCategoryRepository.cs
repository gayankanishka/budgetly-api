using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class TransactionCategoryRepository : GenericRepository<TransactionCategory>, ITransactionCategoryRepository
{
    public TransactionCategoryRepository(ApplicationDbContext context) : base(context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
    }
}