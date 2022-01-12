using Budgetly.Domain.Common;

namespace Budgetly.Domain.Entities;

public class BaseEntity : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
}