using System.Globalization;

namespace RestaurantOrder.Converters
{
    //internal class SelectionChangedEventArgsConverter
    //{
    //}
    public class SelectionChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is SelectionChangedEventArgs args)
                    return args.CurrentSelection?.FirstOrDefault();
            }
            catch
            { 
                return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;
        }
    }

}
