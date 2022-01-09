using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : PaginationQuery, IRequest<PagedResponse<TransactionDto>>
{
}