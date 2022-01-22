using Budgetly.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Providers;

internal interface IDatabaseProvider
{
    public DbContextOptionsBuilder Build(DbContextOptionsBuilder dbContextOptions,
        PersistenceOptions persistenceOptions);
}