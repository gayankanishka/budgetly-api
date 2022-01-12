using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class BudgetItemConfiguration : IEntityTypeConfiguration<BudgetItem>
{
    public void Configure(EntityTypeBuilder<BudgetItem> builder)
    {
        builder.HasData(
            new BudgetItem()
            {
                Id = 1,
                Name = "Food",
                TransactionCategoryId = 2,
                BudgetId = 1,
                Amount = 25000,
                UserId = "auth0|61d9c13ff98384007046a028"
            });
    }
}