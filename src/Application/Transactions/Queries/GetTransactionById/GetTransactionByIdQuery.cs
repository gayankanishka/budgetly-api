using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQuery : IRequest<TransactionDto>
{
    public int Id { get; set; }
}