using Budgetly.Application.Parameters;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQuery : QueryParameters, IRequest<PagedResponse<TransactionCategoryDto>>
{
}