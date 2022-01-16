using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgetHistory;

public class GetBudgetHistoryQuery : IRequest<IEnumerable<BudgetHistoryDto>>
{
}