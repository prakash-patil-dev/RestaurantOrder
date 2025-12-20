using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrder.CustomControls
{
    public class BlinkingButton : Button
    {
        public static readonly BindableProperty IsBlinkingProperty =
            BindableProperty.Create(
                nameof(IsBlinking),
                typeof(bool),
                typeof(BlinkingButton),
                false,
                propertyChanged: OnIsBlinkingChanged);

        public bool IsBlinking
        {
            get => (bool)GetValue(IsBlinkingProperty);
            set => SetValue(IsBlinkingProperty, value);
        }

        public static readonly BindableProperty BlinkColorProperty =
            BindableProperty.Create(
                nameof(BlinkColor),
                typeof(Color),
                typeof(BlinkingButton),
                Colors.Red);   // default blink color

        public Color BlinkColor
        {
            get => (Color)GetValue(BlinkColorProperty);
            set => SetValue(BlinkColorProperty, value);
        }

        private static void OnIsBlinkingChanged(BindableObject bindable, object oldValue, object newValue)
        {

            try
            {
                var button = (BlinkingButton)bindable;
                if ((bool)newValue)
                    button.StartBlink();
                else
                    button.AbortBlink();
            }
            catch
            {

            }
        }

        private void StartBlink()
        {
            try
            {
                var animation = new Animation(v =>
                {
                    // interpolate between BlinkColor and Transparent
                    BorderColor = Color.FromRgba(BlinkColor.Red, BlinkColor.Green, BlinkColor.Blue, v);
                }, 0, 1);

                animation.Commit(this, "BlinkBorder", length: 1000,
                    easing: Easing.Linear,
                    repeat: () => true);
            }
            catch 
            {

            }
        }

        private void AbortBlink()
        {
            try
            {
                this.AbortAnimation("BlinkBorder");
                BorderColor = Color.FromArgb("#f4f0f7"); // reset to default
            }
            catch
            {

            }
        }
    }

}