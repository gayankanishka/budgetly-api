using Budgetly.Infrastructure.Persistence.Enums;
using Budgetly.Infrastructure.Persistence.Providers;

namespace Budgetly.Infrastructure.Persistence.Options;

public class PersistenceOptions
{
    public const string Persistence = "Persistence";
    public string InMemoryConnectionString { get; set; } = string.Empty;
    public string PostgreConnectionString { get; set; } = string.Empty;
    public string SqlServerConnectionString { get; set; } = string.Empty;
    public DataBaseProviderTypes CurrentDatabaseProviderType { get; set; } = DataBaseProviderTypes.InMemory;
    public CacheProviderTypes CurrentCacheProviderType { get; set; } = CacheProviderTypes.InMemory;
}