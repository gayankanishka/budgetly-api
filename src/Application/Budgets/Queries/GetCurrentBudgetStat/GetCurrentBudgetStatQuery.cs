using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;

public class GetCurrentBudgetStatQuery : IRequest<BudgetStatDto>
{
}