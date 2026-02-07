using System.Globalization;

namespace RestaurantOrder.Converters
{
    //internal class CurrencyConvertMultiConverter
    //{
    //}

    public class CurrencyConvertMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return 0m;

            if (!decimal.TryParse(values[0]?.ToString(), out var amount))
                return 0m;

            if (!decimal.TryParse(values[1]?.ToString(), out var rate))
                return 0m;

            if (rate == 0)
                return 0m; // avoid divide by zero

            return amount / rate;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null; // OneWay binding
        }
    }
}
