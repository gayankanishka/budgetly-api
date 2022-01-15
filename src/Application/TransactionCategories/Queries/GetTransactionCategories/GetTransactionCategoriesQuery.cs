using Budgetly.Application.Common.Models;
using Budgetly.Application.Parameters;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQuery : QueryParameters, IRequest<PagedResponse<TransactionCategoryDto>>
{
    public bool? Preset { get; set; }
}