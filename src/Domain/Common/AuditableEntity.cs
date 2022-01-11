namespace Budgetly.Domain.Common;

public class AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}