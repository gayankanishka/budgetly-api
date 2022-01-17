using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreateTransactionCategoryCommand : IRequest<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPreset { get; set; }
}