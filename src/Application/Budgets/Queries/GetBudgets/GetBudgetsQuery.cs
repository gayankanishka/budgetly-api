using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQuery : IRequest<IEnumerable<BudgetDto>>
{
    
}