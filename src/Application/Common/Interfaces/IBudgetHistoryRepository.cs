using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface IBudgetHistoryRepository
{
    IQueryable<BudgetHistory> GetAll();
    IQueryable<BudgetHistory> GetHistoryForPastYear(CancellationToken cancellationToken);
}