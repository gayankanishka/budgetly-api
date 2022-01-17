using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Persistence;
using Budgetly.Infrastructure.Persistence.Imports;
using Budgetly.Infrastructure.Persistence.Options;
using Budgetly.Infrastructure.Persistence.Repositories;
using Budgetly.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgetly.Infrastructure;

/// <summary>
///     Dependency injection extension to configure Infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Configure Infrastructure layer services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseOptions = configuration.GetSection(DatabaseOptions.Database)
            .Get<DatabaseOptions>();

        if (databaseOptions.UseInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(_ =>
                _.UseInMemoryDatabase("BudgetlyDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(_ =>
                _.UseNpgsql(
                    databaseOptions.ConnectionString,
                    a =>
                        a.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        // TODO: GW | Npgsql confusion on mappings, lets keep this for now
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>() 
                                                              ?? throw new InvalidOperationException());
        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IBudgetItemRepository, BudgetItemItemRepository>();

        services.AddTransient<IDateTimeService, DateTimeService>();
        
        services.SeedDatabase();

        return services;
    }
}