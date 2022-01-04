using Budgetly.Domain.Entities;
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
    }

    public DbSet<Budget> Budgets { get; set; } = default!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
}