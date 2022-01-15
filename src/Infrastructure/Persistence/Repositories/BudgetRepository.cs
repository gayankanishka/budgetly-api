using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;

namespace Budgetly.Infrastructure.Persistence.Repositories;

internal class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
{
    public BudgetRepository(ApplicationDbContext context) : base(context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
    }
}