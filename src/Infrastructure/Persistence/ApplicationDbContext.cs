using Budgetly.Domain.Entities;
using Budgetly.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetItemConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetConfiguration());
    }

    public DbSet<Budget> Budgets { get; set; } = default!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
}