using Budgetly.Infrastructure.Persistence.Enums;
using Budgetly.Infrastructure.Persistence.Options;
using Budgetly.Infrastructure.Persistence.Providers;

namespace Budgetly.Infrastructure.Persistence.Factories;

internal class DatabaseFactory : AbstractPersistenceFactory<IDatabaseProvider>
{ 
    public override IDatabaseProvider GetProvider(PersistenceOptions persistenceOptions)
    {
        var provider = persistenceOptions.CurrentDatabaseProviderType;

        if (provider == DataBaseProviderTypes.InMemory)
        {
            return new InMemoryDatabaseProvider();
        }

        if (provider == DataBaseProviderTypes.Postgre)
        {
            return new PostgreProvider();
        }

        if (provider == DataBaseProviderTypes.SqlServer)
        {
            return new SqlServerProvider();
        }

        throw new NotImplementedException();
    }
}