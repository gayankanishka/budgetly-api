using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQuery : PaginationQuery, IRequest<PagedResponse<BudgetDto>>
{
}