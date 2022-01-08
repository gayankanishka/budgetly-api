using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Persistence;
using Budgetly.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgetly.Infrastructure;

/// <summary>
/// Dependency injection extension to configure Infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configure Infrastructure layer services.
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
            services.AddDbContextPool<ApplicationDbContext>(_ =>
                _.UseInMemoryDatabase("BudgetlyDb"));
        }
        else
        {
            services.AddDbContextPool<ApplicationDbContext>(_ =>
                _.UseNpgsql(
                    databaseOptions.ConnectionString,
                    a => 
                        a.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}