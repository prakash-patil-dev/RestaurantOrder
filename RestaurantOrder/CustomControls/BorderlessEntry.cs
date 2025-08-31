using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrder.CustomControls
{
    //internal class BorderlessEntry
    //{
    //}
    public class BorderlessEntry : Entry
    {
        public static readonly BindableProperty NoUnderlineProperty =
            BindableProperty.Create(nameof(NoUnderline), typeof(bool), typeof(BorderlessEntry), true);

        public bool NoUnderline
        {
            get => (bool)GetValue(NoUnderlineProperty);
            set => SetValue(NoUnderlineProperty, value);
        }

  
        public static readonly BindableProperty BackgroundImageSourceProperty =
           BindableProperty.Create(nameof(BackgroundImageSource), typeof(ImageSource), typeof(BorderlessEntry), null);

        public ImageSource BackgroundImageSource
        {
            get => (ImageSource)GetValue(BackgroundImageSourceProperty);
            set => SetValue(BackgroundImageSourceProperty, value);
        }
    }

    //public class ExtendedEntry : Entry
    //{
    //    public static readonly BindableProperty NoUnderlineProperty =
    //        BindableProperty.Create(
    //            nameof(NoUnderline),
    //            typeof(bool),
    //            typeof(ExtendedEntry));

    //    public bool NoUnderline
    //    {
    //        get => (bool)GetValue(NoUnderlineProperty);
    //        set => SetValue(NoUnderlineProperty, value);
    //    }
    //}

}
