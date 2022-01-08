using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQuery : IRequest<IEnumerable<TransactionCategoryDto>>
{
}