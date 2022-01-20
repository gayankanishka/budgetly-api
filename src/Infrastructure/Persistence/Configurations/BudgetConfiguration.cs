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
                ActualExpense = 23000
            },
            new Budget
            {
                Id = 2,
                Name = "car",
                TransactionCategoryId = 3,
                Description = "My car expense budget",
                TargetExpense = 60000,
                UserId = "auth0|61d9c13ff98384007046a028",
                ActualExpense = 135000
            });
    }
}