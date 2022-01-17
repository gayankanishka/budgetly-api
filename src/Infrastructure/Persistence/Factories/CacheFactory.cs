using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Persistence.Enums;
using Budgetly.Infrastructure.Persistence.Options;

namespace Budgetly.Infrastructure.Persistence.Factories;

internal class CacheFactory : AbstractPersistenceFactory<ICacheProvider>
{
    public override ICacheProvider GetProvider(PersistenceOptions persistenceOptions)
    {
        var provider = persistenceOptions.CurrentCacheProviderType;
        
        if (provider == CacheProviderTypes.InMemory)
        {
        }

        if (provider == CacheProviderTypes.Redis)
        {
        }

        throw new NotImplementedException();
    }
}