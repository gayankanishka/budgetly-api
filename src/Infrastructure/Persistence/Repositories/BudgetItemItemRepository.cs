using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class BudgetItemItemRepository : GenericRepository<BudgetItem>, IBudgetItemRepository
{
    public BudgetItemItemRepository(ApplicationDbContext context) : base(context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
    }
}