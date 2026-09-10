namespace CRM.Shared.Infrastructure.Configuration;

public static class EnvLoader
{
    private static bool _isLoaded;

    public static void Load()
    {
        if (_isLoaded)
        {
            return;
        }

        string? envPath = FindEnvFile();
        if (File.Exists(envPath))
        {
            foreach (string line in File.ReadAllLines(envPath))
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#'))
                {
                    continue;
                }

                int separatorIndex = trimmed.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                string key = trimmed[..separatorIndex].Trim();
                string value = trimmed[(separatorIndex + 1)..].Trim().Trim('"', '\'');

                Environment.SetEnvironmentVariable(key, value);
            }
        }

        _isLoaded = true;
    }

    private static string? FindEnvFile()
    {
        for (string? dir = Directory.GetCurrentDirectory(); dir != null; dir = Path.GetDirectoryName(dir))
        {
            string path = Path.Combine(dir, ".env");
            if (File.Exists(path))
            {
                return path;
            }
        }

        for (string? dir = AppContext.BaseDirectory; dir != null; dir = Path.GetDirectoryName(dir))
        {
            string path = Path.Combine(dir, ".env");
            if (File.Exists(path))
            {
                return path;
            }
        }

        return null;
    }
}
