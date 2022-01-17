using Budgetly.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Providers;

public abstract class DatabaseProviderFactory
{
    public static DbContextOptionsBuilder GetProvider(DbContextOptionsBuilder contextOptions, DatabaseOptions databaseOptions)
    {
        var provider = databaseOptions.CurrentProviderType;
        var assemblyName = typeof(ApplicationDbContext).Assembly.FullName;

        if (provider == DataBaseProviderTypes.InMemory)
        {
            return contextOptions.UseInMemoryDatabase(databaseOptions.InMemoryConnectionString);
        }
        else if (provider == DataBaseProviderTypes.Postgre)
        {
            // TODO: GW | Npgsql confusion on mappings, lets keep this for now
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return contextOptions.UseNpgsql(
                databaseOptions.PostgreConnectionString,
                a => a.MigrationsAssembly(assemblyName));
        }
        else if (provider == DataBaseProviderTypes.SqlServer)
        {
            return contextOptions.UseSqlServer(
                databaseOptions.SqlServerConnectionString,
                a => a.MigrationsAssembly(assemblyName));
        }
        
        throw new NotImplementedException();
    }
}