using System.Collections;

namespace CRMSystemMobile.Extensions.HttpExtensions;

public static class BuildQueryExtension
{
    public static string BuildQuery<T>(T filter)
    {
        if (filter == null)
        {
            return string.Empty;
        }

        var properties = filter.GetType().GetProperties();
        var queryParams = new List<string>();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(filter);
            
            if (value == null) continue;
            
            switch (value)
            {
                case string str:
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        continue;
                    }

                    queryParams.Add($"{prop.Name}={Uri.EscapeDataString(str)}");
                    break;

                case IEnumerable list:
                    queryParams.AddRange(list.OfType<object>()
                        .Select(item => Uri.EscapeDataString(FormatValueExtension.FormatValue(item)))
                        .Select(encodedItem => $"{prop.Name}={encodedItem}"));
                    break;

                default:
                    var encodedValue = Uri.EscapeDataString(FormatValueExtension.FormatValue(value));
                    queryParams.Add($"{prop.Name}={encodedValue}");
                    break;
            }
        }

        return string.Join("&", queryParams);
    }
}