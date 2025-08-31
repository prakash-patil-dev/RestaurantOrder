using Android.Graphics;
using Android.Text;
using Android.Views;
using AndroidX.Core.Content;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using RestaurantOrder.CustomControls;
using Android.Content.Res;  // For ColorStateList


namespace RestaurantOrder.Platforms.Android.Handler
{
    internal class BorderlessEntryHandler : EntryHandler
    {
        public static void MapCustomProperties(IElementHandler handler, IElement view)
        {
            if (handler.PlatformView is AndroidX.AppCompat.Widget.AppCompatEditText editText &&
                view is BorderlessEntry entry)
            {
                var context = editText.Context;

                editText.InputType = InputTypes.TextFlagNoSuggestions;
                editText.Gravity = GravityFlags.CenterVertical;
                editText.Typeface = Typeface.Monospace;

                // Remove underline or set default color
                var color = entry.NoUnderline ? Microsoft.Maui.Graphics.Colors.Transparent : Microsoft.Maui.Graphics.Colors.Gray;
                editText.InputType = GetInputTypeFromKeyboard(entry.Keyboard);


                //editText.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(color.ToPlatform());
                editText.BackgroundTintList = ColorStateList.ValueOf(color.ToPlatform());

                var drawable = ContextCompat.GetDrawable(context, Resource.Drawable.textbox_bg);
                editText.Background = drawable;

                editText.SetPadding(40, 10, 20, 10);
                editText.TextCursorDrawable?.SetTint(global::Android.Graphics.Color.ParseColor("#FF5722"));

                // Continue for all other properties...
            }
        }

        private static InputTypes GetInputTypeFromKeyboard(Keyboard keyboard)
        {
            return keyboard switch
            {
                var k when k == Keyboard.Numeric =>
                    InputTypes.ClassNumber | InputTypes.NumberFlagDecimal | InputTypes.NumberFlagSigned,

                var k when k == Keyboard.Telephone =>
                    InputTypes.ClassPhone,

                var k when k == Keyboard.Email =>
                    InputTypes.TextVariationEmailAddress,

                var k when k == Keyboard.Url =>
                    InputTypes.TextVariationUri,

                var k when k == Keyboard.Chat =>
                    InputTypes.ClassText | InputTypes.TextFlagCapSentences | InputTypes.TextFlagAutoCorrect,

                var k when k == Keyboard.Text =>
                    InputTypes.ClassText | InputTypes.TextFlagCapSentences,

                var k when k == Keyboard.Plain =>
                    InputTypes.ClassText,

                _ => InputTypes.ClassText | InputTypes.TextFlagCapSentences | InputTypes.TextFlagAutoCorrect // Default
            };
        }
    
    }
}
