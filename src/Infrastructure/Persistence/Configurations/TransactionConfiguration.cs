using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasData(
            new Transaction()
            {
                Id = 1,
                Name = "Water bill",
                Amount = 1500,
                DateTime = new DateTime(2020, 1, 1),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction()
            {
                Id = 2,
                Name = "Electricity bill",
                Amount = 4500,
                DateTime = new DateTime(2020, 1, 5),
                CategoryId = 4,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction()
            {
                Id = 3,
                Name = "Rent",
                Amount = 30000,
                DateTime = new DateTime(2020, 1, 8),
                CategoryId = 1,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction()
            {
                Id = 4,
                Name = "Groceries",
                Amount = 23000,
                DateTime = new DateTime(2020, 1, 19),
                CategoryId = 2,
                UserId = "auth0|61d9c13ff98384007046a028"
            },
            new Transaction()
            {
                Id = 5,
                Name = "Car payment",
                Amount = 45000,
                DateTime = new DateTime(2020, 1, 21),
                CategoryId = 3,
                UserId = "auth0|61d9c13ff98384007046a028"
            });
    }
}