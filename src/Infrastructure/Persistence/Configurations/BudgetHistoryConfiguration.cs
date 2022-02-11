using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class BudgetHistoryConfiguration : IEntityTypeConfiguration<BudgetHistory>
{
    public void Configure(EntityTypeBuilder<BudgetHistory> builder)
    {
        builder.HasData(
            new BudgetHistory
            {
                Id = 1,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 100000,
                ActualExpense = 100000,
                ActualIncome = 100000,
                Date = new DateTime(2021, 1, 1)
            },
            new BudgetHistory
            {
                Id = 2,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 80000,
                ActualExpense = 100000,
                ActualIncome = 100000,
                Date = new DateTime(2021, 2, 1)
            },
            new BudgetHistory
            {
                Id = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 130000,
                ActualExpense = 130000,
                ActualIncome = 110000,
                Date = new DateTime(2021, 3, 1)
            },
            new BudgetHistory
            {
                Id = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 150000,
                ActualExpense = 130000,
                ActualIncome = 250000,
                Date = new DateTime(2021, 4, 1)
            },
            new BudgetHistory
            {
                Id = 5,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 170000,
                ActualExpense = 190000,
                ActualIncome = 280000,
                Date = new DateTime(2021, 5, 1)
            },
            new BudgetHistory
            {
                Id = 6,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 160000,
                ActualExpense = 150000,
                ActualIncome = 290000,
                Date = new DateTime(2021, 6, 1)
            },
            new BudgetHistory
            {
                Id = 7,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 150000,
                ActualExpense = 130000,
                ActualIncome = 250000,
                Date = new DateTime(2021, 7, 1)
            },
            new BudgetHistory
            {
                Id = 8,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 250000,
                ActualExpense = 230000,
                ActualIncome = 250000,
                Date = new DateTime(2021, 8, 1)
            },
            new BudgetHistory
            {
                Id = 9,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 150000,
                ActualExpense = 180000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 9, 1)
            },
            new BudgetHistory
            {
                Id = 10,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 170000,
                ActualExpense = 20000,
                ActualIncome = 340000,
                Date = new DateTime(2021, 10, 1)
            },
            new BudgetHistory
            {
                Id = 11,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 120000,
                ActualExpense = 200000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 11, 1)
            },
            new BudgetHistory
            {
                Id = 12,
                UserId = "auth0|61d9c13ff98384007046a028",
                TargetExpense = 120000,
                ActualExpense = 50000,
                ActualIncome = 100000,
                Date = new DateTime(2021, 12, 1)
            },
            new BudgetHistory
            {
                Id = 13,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                TargetExpense = 120000,
                ActualExpense = 200000,
                ActualIncome = 200000,
                Date = new DateTime(2022, 02, 1)
            },
            new BudgetHistory
            {
                Id = 14,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                TargetExpense = 100000,
                ActualExpense = 200000,
                ActualIncome = 150000,
                Date = new DateTime(2022, 01, 1)
            },
            new BudgetHistory
            {
                Id = 15,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                TargetExpense = 120000,
                ActualExpense = 180000,
                ActualIncome = 220000,
                Date = new DateTime(2021, 12, 1)
            });
    }
}