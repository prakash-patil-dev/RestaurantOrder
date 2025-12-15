using Application = Microsoft.Maui.Controls.Application;



[assembly: Microsoft.Maui.Controls.Dependency(typeof(RestaurantOrder.Platforms.Windows.Services.CloseAppService))]
namespace RestaurantOrder.Platforms.Windows.Services
{
    public class CloseAppService : ICloseAppService
    {
        public void CloseApp()
        {
            // Get the native WinUI window from the MAUI Window
            var mauiWindow = Application.Current.Windows.FirstOrDefault();
            var nativeWindow = mauiWindow?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;

            // Close the native window
            nativeWindow?.Close();
        }

    }

}
