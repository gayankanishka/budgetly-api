using Budgetly.Domain.Entities;
using Budgetly.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder
            .Property(_ => _.Type)
            .HasConversion(
                _ => _.ToString(),
                _ => (TransactionTypes)Enum.Parse(typeof(TransactionTypes), _));

        builder.HasData(
            new Transaction
            {
                Id = 1,
                Name = "Monthly salary from Budgetly",
                Amount = 320000,
                DateTime = new DateTime(2022, 1, 1),
                CategoryId = 6,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Income,
                IsRecurring = true
            },
            new Transaction
            {
                Id = 2,
                Name = "Electricity bill",
                Amount = 4500,
                DateTime = new DateTime(2022, 1, 5),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 3,
                Name = "Rent",
                Amount = 30000,
                DateTime = new DateTime(2022, 1, 8),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 4,
                Name = "Groceries",
                Amount = 23000,
                DateTime = new DateTime(2022, 1, 19),
                CategoryId = 2,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 1,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 5,
                Name = "Car installment payment",
                Amount = 50000,
                DateTime = new DateTime(2020, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 6,
                Name = "Car insurance",
                Amount = 10000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 7,
                Name = "Car petrol",
                Amount = 15000,
                DateTime = new DateTime(2022, 1, 25),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 8,
                Name = "Doctor appointment",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 28),
                CategoryId = 5,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 9,
                Name = "Medicine",
                Amount = 2000,
                DateTime = new DateTime(2022, 1, 28),
                CategoryId = 5,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 10,
                Name = "Monthly salary from Budgetly",
                Amount = 320000,
                DateTime = new DateTime(2022, 2, 1),
                CategoryId = 6,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Income,
                IsRecurring = true
            },
            new Transaction
            {
                Id = 11,
                Name = "Daddy musical show tickets",
                Amount = 5000,
                DateTime = new DateTime(2022, 2, 1),
                CategoryId = 2,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 12,
                Name = "Water bill",
                Amount = 2000,
                DateTime = new DateTime(2022, 2, 2),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 13,
                Name = "Electricity bill",
                Amount = 5000,
                DateTime = new DateTime(2022, 2, 5),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 14,
                Name = "Phone bill",
                Amount = 5200,
                DateTime = new DateTime(2022, 2, 8),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 15,
                Name = "Car tire check",
                Amount = 5000,
                DateTime = new DateTime(2022, 2, 15),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 16,
                Name = "Car oil change",
                Amount = 5000,
                DateTime = new DateTime(2022, 2, 16),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 17,
                Name = "Car engine check",
                Amount = 3000,
                DateTime = new DateTime(2022, 2, 16),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 18,
                Name = "Cloth shopping",
                Amount = 15000,
                DateTime = new DateTime(2022, 2, 22),
                CategoryId = 7,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 19,
                Name = "Shoe shopping",
                Amount = 10000,
                DateTime = new DateTime(2022, 2, 22),
                CategoryId = 7,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 20,
                Name = "Car wash",
                Amount = 3000,
                DateTime = new DateTime(2022, 2, 26),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028",
                BudgetId = 2,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 21,
                Name = "AWS fee",
                Amount = 3000,
                DateTime = new DateTime(2022, 2, 28),
                CategoryId = 7,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 22,
                Name = "Azure fee",
                Amount = 2000,
                DateTime = new DateTime(2022, 2, 28),
                CategoryId = 7,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 23,
                Name = "Food",
                Amount = 4500,
                DateTime = new DateTime(2022, 2, 28),
                CategoryId = 1,
                UserId = "auth0|61d9c13ff98384007046a028",
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 24,
                Name = "Groceries",
                Amount = 5000,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 25,
                Name = "Cookware",
                Amount = 2000,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 26,
                Name = "Clothes",
                Amount = 7000,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 27,
                Name = "Bread",
                Amount = 500,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 28,
                Name = "Groceries",
                Amount = 1200,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            },
            new Transaction
            {
                Id = 29,
                Name = "Food",
                Amount = 800,
                DateTime = new DateTime(2022, 2, 21),
                CategoryId = 8,
                UserId = "auth0|61e10a1f7a958c0070a4c713",
                BudgetId = 3,
                Type = TransactionTypes.Expense
            });
    }
}