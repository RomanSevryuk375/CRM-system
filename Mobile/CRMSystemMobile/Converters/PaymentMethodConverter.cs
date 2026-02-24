using System.Globalization;
using Shared.Enums;

namespace CRMSystemMobile.Converters;

public class PaymentMethodConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            PaymentMethodEnum method => method switch
            {
                PaymentMethodEnum.Card => "Картой",
                PaymentMethodEnum.Cash => "Наличными",
                PaymentMethodEnum.Erip => "ЕРИП",
                _ => "Неизвестно"
            },
            int methodId => methodId switch
            {
                1 => "Картой",
                2 => "Наличными",
                3 => "ЕРИП",
                _ => "Неизвестно"
            },
            _ => "—"
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}