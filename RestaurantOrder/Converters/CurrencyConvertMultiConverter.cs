using System.Globalization;

namespace RestaurantOrder.Converters
{
    //internal class CurrencyConvertMultiConverter
    //{
    //}

    //public class CurrencyConvertMultiConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (values == null || values.Length < 2)
    //            return 0d;

    //        if (!double.TryParse(values[0]?.ToString(), out var amount))
    //            return 0d;

    //        if (!double.TryParse(values[1]?.ToString(), out var rate))
    //            return 0d;

    //        if (rate == 0)
    //            return 0d; // avoid divide by zero

    //        return amount / rate;
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        return null; // OneWay binding
    //    }
    //}

    public class CurrencyConvertMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return 0m;

            if (values[0] == null || values[1] == null)
                return 0m;

            if (!decimal.TryParse(values[0].ToString(), NumberStyles.Any, culture, out var amount))
                return 0m;

            if (!decimal.TryParse(values[1].ToString(), NumberStyles.Any, culture, out var rate))
                return 0m;

            if (rate <= 0)
                return 0m; // avoid divide by zero or invalid rate

            // Currency conversion
            var result = amount / rate;

            return result;
            // Round to 2 decimal places (important for money)
            //return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null; // OneWay binding
        }
    }

}
