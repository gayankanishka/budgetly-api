using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Persistence.Options;
using Budgetly.Infrastructure.Persistence.Providers;

namespace Budgetly.Infrastructure.Persistence.Factories;

internal abstract class AbstractPersistenceFactory<T>
{
    public abstract T GetProvider(PersistenceOptions persistenceOptions);
    
    public static AbstractPersistenceFactory<T> CreateFactory()
    {
        if (typeof(T) == typeof(IDatabaseProvider))
        {
            return new DatabaseFactory() as AbstractPersistenceFactory<T>;
        }

        if (typeof(T) == typeof(ICacheProvider))
        {
            return new CacheFactory() as AbstractPersistenceFactory<T>;
        }

        throw new NotImplementedException();
    }
}