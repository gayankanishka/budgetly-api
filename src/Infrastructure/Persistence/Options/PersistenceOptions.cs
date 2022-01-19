using Budgetly.Infrastructure.Persistence.Enums;

namespace Budgetly.Infrastructure.Persistence.Options;

public class PersistenceOptions
{
    public const string Persistence = "Persistence";
    public string InMemoryConnectionString { get; set; } = string.Empty;
    public string PostgreSqlConnectionString { get; set; } = string.Empty;
    public string SqlServerConnectionString { get; set; } = string.Empty;
    public DataBaseProviderTypes CurrentDatabaseProvider { get; set; } = DataBaseProviderTypes.InMemory;
    public CacheProviderTypes CurrentCacheProvider { get; set; } = CacheProviderTypes.InMemory;
}