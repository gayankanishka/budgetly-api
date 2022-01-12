using System.Reflection;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _user;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService user)
        : base(options)
    {
        _user = user;
    }
    
    public DbSet<Budget> Budgets { get; set; } = default!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _user.UserId;
                    entry.Entity.Created = DateTimeOffset.UtcNow;
                    entry.Entity.UserId = _user.UserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _user.UserId;
                    entry.Entity.LastModified = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}