using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace CRM.Shared.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    private static readonly Regex PlaceholderRegex = new(@"\$\{([A-Za-z0-9_]+)(?::-([^}]*))?\}", RegexOptions.Compiled);

    public static IConfiguration ExpandEnvironmentVariables(this IConfiguration configuration)
    {
        foreach (KeyValuePair<string, string?> pair in configuration.AsEnumerable())
        {
            if (pair.Value is not { } value || !value.Contains("${"))
            {
                continue;
            }

            string expanded = PlaceholderRegex.Replace(value, match =>
            {
                string varName = match.Groups[1].Value;
                string? envValue = Environment.GetEnvironmentVariable(varName);

                if (!string.IsNullOrEmpty(envValue))
                {
                    return envValue;
                }

                if (match.Groups[2].Success)
                {
                    return match.Groups[2].Value;
                }

                return match.Value;
            });

            configuration[pair.Key] = expanded;
        }

        return configuration;
    }
}
