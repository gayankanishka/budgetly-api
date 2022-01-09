using Budgetly.Application.Parameters;
using Budgetly.Application.Responses;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQuery : QueryParameters, IRequest<PagedResponse<TransactionCategoryDto>>
{
}