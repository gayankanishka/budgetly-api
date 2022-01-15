using FluentValidation;

namespace Budgetly.Application.Budgets.Commands.UpdateBudgetItem;

public class UpdateBudgetItemCommandValidator : AbstractValidator<UpdateBudgetItemCommand>
{
    public UpdateBudgetItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Id must be a valid value");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Name must be less than 50 characters");
        RuleFor(x => x.Description)
            .MaximumLength(250)
            .WithMessage("Description must be less than 250 characters");

        RuleFor(x => x.TransactionCategoryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("CategoryId must be a valid value");

        RuleFor(x => x.TargetExpense)
            .NotNull()
            .WithMessage("TargetExpense is required");
    }
}