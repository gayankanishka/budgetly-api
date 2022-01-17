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

        RuleFor(x => x.TransactionCategoryId)
            .NotNull()
            .WithMessage("CategoryId is required");

        RuleFor(x => x.TargetExpense)
            .GreaterThan(0)
            .WithMessage("TargetExpense is required and must be grater than or equal to 0");
    }
}