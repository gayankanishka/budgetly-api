using Budgetly.Application.Parameters;
using Budgetly.Application.Responses;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQuery : QueryParameters, IRequest<PagedResponse<BudgetDto>>
{
}