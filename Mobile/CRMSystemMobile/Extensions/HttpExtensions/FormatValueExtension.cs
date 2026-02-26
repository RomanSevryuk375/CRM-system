using System.Globalization;

namespace CRMSystemMobile.Extensions.HttpExtensions;

public static class FormatValueExtension
{
    public static string FormatValue(object? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        if (value.GetType().IsEnum)
        {
            return ((int)value).ToString();
        }

        return value switch
        {
            DateTime date => date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture),
            DateOnly dateOnly => dateOnly.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            _ => value.ToString() ?? string.Empty
        };
    }
}