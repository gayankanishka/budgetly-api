using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategoryById;

public class GetTransactionCategoryByIdQuery : IRequest<TransactionCategoryDto>
{
    public GetTransactionCategoryByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}