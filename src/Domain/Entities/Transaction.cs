using Budgetly.Domain.Common;
using Budgetly.Domain.Enums;

namespace Budgetly.Domain.Entities;

public class Transaction : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Amount { get; set; }
    public TransactionTypes Type { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public string? Note { get; set; }
    public int CategoryId { get; set; }
    public TransactionCategory Category { get; set; } // TODO: GK | can a transaction have more than one category?
    public bool IsRecurring { get; set; }
    public string UserId { get; set; }
}