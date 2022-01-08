using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategoryById;

public class GetTransactionCategoryByIdQuery : IRequest<TransactionCategoryDto?>
{
    public int Id { get; private set; }

    public GetTransactionCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
