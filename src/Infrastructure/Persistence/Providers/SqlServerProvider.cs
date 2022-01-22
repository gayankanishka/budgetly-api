using Budgetly.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Providers;

public class SqlServerProvider : IDatabaseProvider
{
    public DbContextOptionsBuilder Build(DbContextOptionsBuilder dbContextOptions,
        PersistenceOptions persistenceOptions)
    {
        var assemblyName = typeof(ApplicationDbContext).Assembly.FullName;

        return dbContextOptions.UseSqlServer(
            persistenceOptions.SqlServerConnectionString,
            a => a.MigrationsAssembly(assemblyName));
    }
}