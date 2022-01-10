using Budgetly.Application.Parameters;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQuery : QueryParameters, IRequest<PagedResponse<BudgetDto>>
{
}