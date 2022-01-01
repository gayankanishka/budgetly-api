namespace Budgetly.Infrastructure.Persistence;

public class DatabaseOptions
{
    public const string Database = "Database";

    public bool UseInMemoryDatabase { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}