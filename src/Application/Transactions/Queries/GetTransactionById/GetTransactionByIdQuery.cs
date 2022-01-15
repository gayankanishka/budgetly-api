using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQuery : IRequest<TransactionDto>
{
    public GetTransactionByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}