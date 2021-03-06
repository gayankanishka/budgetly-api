using FluentValidation;

namespace Budgetly.Application.Budgets.Commands.UpdateBudget;

public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandValidator()
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

        RuleFor(x => x.TargetExpense)
            .GreaterThan(0)
            .WithMessage("TargetExpense is required and must be grater than or equal to 0");
    }
}