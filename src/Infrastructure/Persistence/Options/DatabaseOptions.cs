using Budgetly.Infrastructure.Persistence.Providers;

namespace Budgetly.Infrastructure.Persistence.Options;

public class DatabaseOptions
{
    public const string Database = "Database";
    public string InMemoryConnectionString { get; set; } = string.Empty;
    public string PostgreConnectionString { get; set; } = string.Empty;
    public string SqlServerConnectionString { get; set; } = string.Empty;
    public DataBaseProviderTypes CurrentProviderType { get; set; } = DataBaseProviderTypes.InMemory;
}