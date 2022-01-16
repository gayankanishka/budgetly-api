using System.Reflection;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Common;
using Budgetly.Domain.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _user;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService user, IDomainEventService domainEventService)
        : base(options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _user = user ?? throw new ArgumentNullException(nameof(user));
        _domainEventService = domainEventService;
        _domainEventService = domainEventService ?? throw new ArgumentNullException(nameof(domainEventService));
    }

    public DbSet<BudgetItem> BudgetItems { get; set; } = default!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
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
        
        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var results = await base.SaveChangesAsync(cancellationToken);
        
        await DispatchEvents(events);

        return results;
    }
    
    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}