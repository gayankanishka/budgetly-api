using Budgetly.Domain.Dtos;
using Budgetly.Domain.Enums;
using MediatR;

namespace Budgetly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public TransactionTypes Type { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public string? Note { get; set; }
    public int CategoryId { get; set; }
    public bool IsRecurring { get; set; }
}