using Budgetly.Application.Parameters;
using Budgetly.Application.Responses;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : QueryParameters, IRequest<PagedResponse<TransactionDto>>
{
    public string? Name { get; set; }
    public DateTimeOffset? StartDate { get; set; } =
        new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1);
    public DateTimeOffset? EndDate { get; set; } =
        new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 
            DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month));
    public bool? Recurring { get; set; }
}