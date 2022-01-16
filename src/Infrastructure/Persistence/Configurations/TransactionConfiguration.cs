using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.HasData(
            new Transaction
            {
                Id = 1,
                Name = "Water bill",
                Amount = 1500,
                DateTime = new DateTime(2022, 1, 1),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 2,
                Name = "Electricity bill",
                Amount = 4500,
                DateTime = new DateTime(2022, 1, 5),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 3,
                Name = "Rent",
                Amount = 30000,
                DateTime = new DateTime(2022, 1, 8),
                CategoryId = 1,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 4,
                Name = "Groceries",
                Amount = 23000,
                DateTime = new DateTime(2022, 1, 19),
                CategoryId = 2,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 5,
                Name = "Car payment",
                Amount = 45000,
                DateTime = new DateTime(2020, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 6,
                Name = "Car insurance",
                Amount = 10000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 7,
                Name = "Car gas",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 8,
                Name = "Car service",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 9,
                Name = "Car oil",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 10,
                Name = "Car tires",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 11,
                Name = "Car wash",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 12,
                Name = "Car wax",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 13,
                Name = "Car tire rotation",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 14,
                Name = "Car engine check",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 15,
                Name = "Car tire check",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 16,
                Name = "Car oil change",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 17,
                Name = "Car tire rotation",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 18,
                Name = "Car engine check",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 19,
                Name = "Car tire check",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 20,
                Name = "Car oil change",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 21,
                Name = "Car tire rotation",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction
            {
                Id = 22,
                Name = "Car engine check",
                Amount = 5000,
                DateTime = new DateTime(2022, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            });
    }
}