using Microsoft.Extensions.DependencyInjection;

namespace Budgetly.Infrastructure.Persistence.Imports;

public static class MigrationManager
{
    public static IServiceCollection SeedDatabase(this IServiceCollection serviceProvider)
    {
        using var appContext = serviceProvider.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
        try
        {
            appContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            //Log errors or do anything you think it's needed
            throw;
        }

        return serviceProvider;
    }
}