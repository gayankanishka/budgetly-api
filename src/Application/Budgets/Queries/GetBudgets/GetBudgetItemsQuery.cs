using Budgetly.Application.Common.Models;
using Budgetly.Application.Parameters;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetItemsQuery : IRequest<IEnumerable<BudgetItemDto>>
{
    public string? Name { get; set; }
    public int? TransactionCategoryId { get; set; }
}