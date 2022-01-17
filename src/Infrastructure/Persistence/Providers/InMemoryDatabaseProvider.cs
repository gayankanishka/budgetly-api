using Budgetly.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Providers;

public class InMemoryDatabaseProvider : IDatabaseProvider
{
    public DbContextOptionsBuilder Build(DbContextOptionsBuilder contextOptionsBuilder, PersistenceOptions persistenceOptions)
    {
        return contextOptionsBuilder.UseInMemoryDatabase(persistenceOptions.InMemoryConnectionString);
    }
}