using Budgetly.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Providers;

public class PostgreProvider : IDatabaseProvider
{
    public DbContextOptionsBuilder Build(DbContextOptionsBuilder dbContextOptions, PersistenceOptions persistenceOptions)
    {
        // TODO: GW | Npgsql confusion on mappings, lets keep this for now
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        var assemblyName = typeof(ApplicationDbContext).Assembly.FullName;

        return dbContextOptions.UseNpgsql(
            persistenceOptions.PostgreConnectionString,
            a => a.MigrationsAssembly(assemblyName));
    }
}