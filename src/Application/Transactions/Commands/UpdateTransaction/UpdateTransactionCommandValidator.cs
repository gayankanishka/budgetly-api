using FluentValidation;

namespace Budgetly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Id must be a valid value");

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Name must be less than 50 characters");

        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount is required");

        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage("Transaction type is required")
            .IsInEnum()
            .WithMessage("Transaction type is invalid");

        RuleFor(x => x.DateTime)
            .NotNull()
            .WithMessage("DateTime is not valid");

        RuleFor(x => x.Note)
            .MaximumLength(250)
            .WithMessage("Note must be less than 250 characters");

        RuleFor(x => x.CategoryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("CategoryId must be a valid value");

        RuleFor(x => x.IsRecurring)
            .NotNull()
            .WithMessage("IsRecurring is required");
    }
}