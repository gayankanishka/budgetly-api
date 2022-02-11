using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.HasData(
            new Budget
            {
                Id = 1,
                Name = "Food",
                TransactionCategoryId = 1,
                TargetExpense = 25000,
                UserId = "auth0|61d9c13ff98384007046a028",
                ActualExpense = 4500
            },
            new Budget
            {
                Id = 2,
                Name = "Car",
                TransactionCategoryId = 3,
                Description = "My car expense budget",
                TargetExpense = 60000,
                UserId = "auth0|61d9c13ff98384007046a028",
                ActualExpense = 15000
            },
            new Budget
            {
                Id = 3,
                Name = "Shopping Budget",
                TransactionCategoryId = 1,
                TargetExpense = 70000,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                ActualExpense = 50000
            });
    }
}