using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrder.Converters
{
    //internal class VisibilityToListConverter
    //{
    //}
    public class VisibilityToListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 &&
                values[0] is bool isVisible &&
                values[1] is IEnumerable list)
            {
                return isVisible ? list : Enumerable.Empty<object>();
            }

            return Enumerable.Empty<object>();
        }


        //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (values.Length == 2 && values[0] is bool isVisible && values[1] != null)
        //    {
        //        return isVisible ? values[1] : new();
        //    }
        //    return new();
        //}

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            =>   new object[2]; 
    }
}
