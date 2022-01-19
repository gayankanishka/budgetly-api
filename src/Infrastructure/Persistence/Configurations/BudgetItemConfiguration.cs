using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class BudgetItemConfiguration : IEntityTypeConfiguration<BudgetItem>
{
    public void Configure(EntityTypeBuilder<BudgetItem> builder)
    {
        builder.HasData(
            new BudgetItem
            {
                Id = 1,
                Name = "Food",
                TransactionCategoryId = 1,
                TargetExpense = 25000,
                UserId = "auth0|61d9c13ff98384007046a028",
                ActualExpense = 23000
            },
            new BudgetItem
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