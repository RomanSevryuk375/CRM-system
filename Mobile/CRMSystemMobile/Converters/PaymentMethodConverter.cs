using System.Globalization;
using Shared.Enums;

namespace CRMSystemMobile.Converters;

public class PaymentMethodConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is PaymentMethodEnum method)
        {
            return method switch
            {
                PaymentMethodEnum.Card => "Картой",
                PaymentMethodEnum.Cash => "Наличными",
                PaymentMethodEnum.Erip => "ЕРИП",
                _ => "Неизвестно"
            };
        }

        if (value is int methodId)
        {
            return methodId switch
            {
                1 => "Картой",
                2 => "Наличными",
                3 => "ЕРИП",
                _ => "Неизвестно"
            };
        }

        return "—";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}