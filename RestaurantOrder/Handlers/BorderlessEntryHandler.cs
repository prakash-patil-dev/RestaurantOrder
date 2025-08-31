using Microsoft.Maui.Handlers;
using RestaurantOrder.CustomControls;
using Microsoft.Maui.Platform;
#if WINDOWS
//using Microsoft.UI.Xaml.Media;
using WinBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;
#endif
#if ANDROID
using Android.Widget;
using Android.Text;
using Android.Views;
using Android.Graphics;
using Android.Util; // Fix for TypedValue and ComplexUnitType
#endif


namespace RestaurantOrder.Handlers
{
    public class BorderlessEntryHandler : EntryHandler
    {
        public static IPropertyMapper<BorderlessEntry, BorderlessEntryHandler> PropertyMapper =
            new PropertyMapper<BorderlessEntry, BorderlessEntryHandler>(EntryHandler.Mapper)
            {
                [nameof(BorderlessEntry.NoUnderline)] = MapNoUnderlineProperty,
               // [nameof(BorderlessEntry)] = MapBackgroundImage,
                [nameof(BorderlessEntry.BackgroundImageSource)] = MapBackgroundImage,
            };

        public BorderlessEntryHandler() : base(PropertyMapper) { }

   
        public static void MapNoUnderlineProperty(BorderlessEntryHandler handler, BorderlessEntry entry)
        {
            try
            {
#if ANDROID
            if (handler.PlatformView is Android.Widget.EditText editText)
            {
                var color = entry.NoUnderline ? Microsoft.Maui.Graphics.Colors.Transparent : Microsoft.Maui.Graphics.Colors.Gray;
                editText.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(color.ToPlatform());
            }
#elif IOS || MACCATALYST
                if (handler.PlatformView is UIKit.UITextField textField)
                {
                    if (entry.NoUnderline)
                    {
                        textField.BorderStyle = UIKit.UITextBorderStyle.None;
                        textField.Layer.BorderWidth = 0;
                    }
                    else
                    {
                        textField.BorderStyle = UIKit.UITextBorderStyle.Line;
                        textField.Layer.BorderWidth = 1;
                    }
                }
#elif WINDOWS
            if (handler.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
            {
                textBox.BorderThickness = entry.NoUnderline
                    ? new Microsoft.UI.Xaml.Thickness(0)
                    : new Microsoft.UI.Xaml.Thickness(1);
            }
#endif
            }
            catch (Exception ex)
            {
            }
        }


        public static void MapBackgroundImage(BorderlessEntryHandler handler, BorderlessEntry entry)
        {
            try
            {
#if ANDROID
                 if (handler.PlatformView is Android.Widget.EditText editText)
                 {
                    editText.SetBackgroundColor(Android.Graphics.Color.Black);
                    editText.InputType = InputTypes.TextFlagNoSuggestions;
                    editText.Gravity = GravityFlags.CenterVertical;
                    editText.Typeface = Typeface.Monospace;
                    editText.SetPadding(40, 5, 20, 5);
                 }
#elif IOS || MACCATALYST
                if (handler.PlatformView is UIKit.UITextField textField)
                {
                    var image = UIKit.UIImage.FromBundle("textbox_bg");
                    if (image != null)
                    {
                        var backgroundView = new UIKit.UIView(textField.Bounds)
                        {
                            BackgroundColor = UIKit.UIColor.FromPatternImage(image)
                        };
                        textField.InsertSubview(backgroundView, 0);
                    }
                }
#elif WINDOWS
    if (handler.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
    {
      //  var uri = new Uri("ms-appx:///Resources/Images/textbox_bg.png");
           var uri = new Uri("ms-appx:///textbox_bg.png"); 
        var imageSource = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(uri);
        var brush = new Microsoft.UI.Xaml.Media.ImageBrush
        {
            ImageSource = imageSource,
            Stretch = Microsoft.UI.Xaml.Media.Stretch.UniformToFill
        };
        textBox.Background = brush;  
      
        // Remove border and focus visuals
        textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
        //textBox.FocusVisualPrimaryBrush = new SolidColorBrush(Windows.UI.Colors.Transparent);
        //textBox.FocusVisualSecondaryBrush = new SolidColorBrush(Windows.UI.Colors.Transparent);
    
        textBox.FocusVisualPrimaryBrush = new WinBrush(Microsoft.UI.Colors.Transparent);
        textBox.FocusVisualSecondaryBrush = new WinBrush(Microsoft.UI.Colors.Transparent);

  
    }

#endif
            }
            catch (Exception ex)
            {
            }
        }


        

        //        public static void MapBackgroundImage(BorderlessEntryHandler handler, BorderlessEntry entry)
        //        {
        //#if ANDROID
        //            if (handler.PlatformView is Android.Widget.EditText editText)
        //            {
        //                var context = editText.Context;
        //                var imageId = context.Resources.GetIdentifier("entry_bg", "drawable", context.PackageName);
        //                var drawable = context.GetDrawable(imageId);
        //                if (drawable != null)
        //                editText.Background = drawable;
        //                    //editText.SetBackground(drawable);
        //            }
        //#elif IOS || MACCATALYST
        //            if (handler.PlatformView is UIKit.UITextField textField)
        //            {
        //                var image = UIKit.UIImage.FromBundle("entry_bg");
        //                if (image != null)
        //                {
        //                    var backgroundView = new UIKit.UIView(textField.Bounds);
        //                    backgroundView.BackgroundColor = UIKit.UIColor.FromPatternImage(image);
        //                    textField.InsertSubview(backgroundView, 0);
        //                }
        //            }
        //#elif WINDOWS
        //            if (handler.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
        //            {
        //                var brush = new Microsoft.UI.Xaml.Media.ImageBrush
        //                {
        //                    ImageSource = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(
        //                        new Uri("ms-appx:///Resources/entry_bg.png"))
        //                };
        //                textBox.Background = brush;
        //            }
        //#endif
        //        }
    }
}