using Microsoft.Extensions.Configuration;

namespace CRM.Shared.Infrastructure.Configuration;

public static class ConnectionStringHelper
{
    private const string DefaultConnectionString =
        "Host=localhost;Port=5438;Database=AUTOService;Username=postgres228;Password=roman228";

    public static string GetConnectionString(IConfiguration? configuration = null, string? connectionName = null)
    {
        EnvLoader.Load();

        if (configuration is not null)
        {
            string? fromConfig = !string.IsNullOrWhiteSpace(connectionName)
                ? configuration.GetConnectionString(connectionName)
                : null;

            fromConfig ??= configuration.GetConnectionString("Database")
                ?? configuration.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(fromConfig) && !fromConfig.Contains("${"))
            {
                return fromConfig;
            }
        }

        string? fromEnv = !string.IsNullOrWhiteSpace(connectionName)
            ? Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionName}")
            : null;

        fromEnv ??= Environment.GetEnvironmentVariable("ConnectionStrings__Database")
            ?? Environment.GetEnvironmentVariable("DATABASE_URL");

        if (!string.IsNullOrWhiteSpace(fromEnv))
        {
            return fromEnv;
        }

        string? host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        string? port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5438";
        string? db = Environment.GetEnvironmentVariable("POSTGRES_DB");
        string? user = Environment.GetEnvironmentVariable("POSTGRES_USER");
        string? pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

        if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(db))
        {
            return $"Host={host};Port={port};Database={db};Username={user};Password={pass}";
        }

        return DefaultConnectionString;
    }
}
