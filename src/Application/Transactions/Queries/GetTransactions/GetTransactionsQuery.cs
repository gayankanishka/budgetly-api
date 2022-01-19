using Budgetly.Application.Common.Models;
using Budgetly.Application.Parameters;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Enums;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : QueryParameters, IRequest<PagedResponse<TransactionDto>>
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public bool? Recurring { get; set; }
    public TransactionTypes? TransactionType { get; set; }
    public int? CategoryId { get; set; }
}