using System.Reflection;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Common;
using Budgetly.Domain.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService user,
        IDomainEventService domainEventService, IDateTimeService dateTimeService)
        : base(options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _currentUserService = user ?? throw new ArgumentNullException(nameof(user));
        _domainEventService = domainEventService;
        _domainEventService = domainEventService ?? throw new ArgumentNullException(nameof(domainEventService));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public DbSet<BudgetItem> BudgetItems => Set<BudgetItem>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<TransactionCategory>().HasQueryFilter(o => 
            o.UserId == _currentUserService.UserId || o.IsPreset);
        modelBuilder.Entity<Transaction>().HasQueryFilter(o => o.UserId == _currentUserService.UserId);
        modelBuilder.Entity<BudgetItem>().HasQueryFilter(o => o.UserId == _currentUserService.UserId);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTimeService.UtcNow;
                    entry.Entity.UserId = _currentUserService.UserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTimeService.UtcNow;
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