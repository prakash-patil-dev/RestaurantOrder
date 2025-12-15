using RestaurantOrder.Models;
using System.Globalization;

namespace RestaurantOrder.Converters
{
    //class SelectedItemComparerConverter
    //{
    //}
    public class SelectedItemComparerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collectionView = parameter as CollectionView;
            return (INVLINE)collectionView?.SelectedItems.FirstOrDefault() == value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
