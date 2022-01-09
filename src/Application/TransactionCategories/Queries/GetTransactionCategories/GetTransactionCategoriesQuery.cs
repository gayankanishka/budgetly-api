using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQuery : PaginationQuery, IRequest<PagedResponse<TransactionCategoryDto>>
{
}