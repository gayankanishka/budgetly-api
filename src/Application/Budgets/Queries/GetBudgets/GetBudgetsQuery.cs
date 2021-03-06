using Budgetly.Application.Common.Models;
using Budgetly.Application.Parameters;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQuery : QueryParameters, IRequest<PagedResponse<BudgetDto>>
{
    public bool? Exceeded { get; set; }
    public int? TransactionCategoryId { get; set; }
}