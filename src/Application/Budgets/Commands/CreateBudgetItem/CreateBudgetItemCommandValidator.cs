using FluentValidation;

namespace Budgetly.Application.Budgets.Commands.CreateBudgetItem;

public class CreateBudgetItemCommandValidator : AbstractValidator<CreateBudgetItemCommand>
{
    public CreateBudgetItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Name must be less than 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage("Description must be less than 200 characters");

        RuleFor(x => x.TargetExpense)
            .NotNull()
            .WithMessage("TargetExpense is required");

        RuleFor(x => x.CategoryId)
            .NotNull()
            .WithMessage("CategoryId is required");
    }
}