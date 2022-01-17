using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Persistence.Enums;
using Budgetly.Infrastructure.Persistence.Options;

namespace Budgetly.Infrastructure.Persistence.Factories;

internal class CacheFactory : AbstractPersistenceFactory<ICacheProvider>
{
    // TODO: Ideally this will create a cache provider based on the configuration. This would be future work.
    public override ICacheProvider GetProvider(PersistenceOptions persistenceOptions)
    {
        var provider = persistenceOptions.CurrentCacheProviderType;
        
        if (provider == CacheProviderTypes.InMemory)
        {
            throw new NotImplementedException();
        }

        if (provider == CacheProviderTypes.Redis)
        {
            throw new NotImplementedException();
        }

        throw new NotImplementedException();
    }
}