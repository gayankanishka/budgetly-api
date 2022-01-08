using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;

public class UpdateTransactionCategoryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; }
    public bool IsPreset { get; set; }
}