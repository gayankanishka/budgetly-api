using FluentValidation;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreateTransactionCategoryCommandValidator : AbstractValidator<CreatTransactionCategoryCommand>
{
    public CreateTransactionCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Name must be less than 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage("Description must be less than 200 characters");

        RuleFor(x => x.IsPreset)
            .NotNull()
            .WithMessage("IsPreset is required");
    }
}