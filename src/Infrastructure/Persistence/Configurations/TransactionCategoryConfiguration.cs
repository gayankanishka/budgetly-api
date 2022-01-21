using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategory>
{
    public void Configure(EntityTypeBuilder<TransactionCategory> builder)
    {
        builder.HasData(
            new TransactionCategory
            {
                Id = 1,
                Name = "Food",
                Description = "Food and Groceries",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 2,
                Name = "Entertainment",
                Description = "Entertainment",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 3,
                Name = "Transportation",
                Description = "Transportation",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 4,
                Name = "Utilities",
                Description = "Utility bills.",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 5,
                Name = "Health",
                Description = "Health costs.",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 6,
                Name = "Salary",
                Description = "Monthly salary from the job.",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new TransactionCategory
            {
                Id = 7,
                Name = "Other",
                Description = "All other transactions.",
                IsPreset = true,
                UserId = "auth0|61d9c13ff98384007046a028"
            }
        );
    }
}