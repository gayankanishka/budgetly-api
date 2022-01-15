using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgetly.Infrastructure.Persistence.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasData(
            new Budget()
            {
                Id = 1,
                Name = "January Budget",
                Description = "This is the first budget for January",
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 31),
                BudgeLimit = 200000,
                UserId = ""
            });
    }
}