using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using RestaurantOrder.CustomControls;
using RestaurantOrder.Handlers;
using Syncfusion.Maui.Core.Hosting;
using System.Runtime.InteropServices;

namespace RestaurantOrder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups() 
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                    
                })
                .ConfigureSyncfusionCore().ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated(window =>
                    {
                        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

                        // ✅ Maximize window (title bar stays)
                        ShowWindow(hwnd, SW_MAXIMIZE);
                    });
                });
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if ANDROID
            builder.Services.AddSingleton<ICloseAppService, RestaurantOrder.Platforms.Android.Services.CloseAppService>();
#elif WINDOWS
            builder.Services.AddSingleton<ICloseAppService, RestaurantOrder.Platforms.Windows.Services.CloseAppService>();
#elif IOS
            builder.Services.AddSingleton<ICloseAppService, RestaurantOrder.Platforms.iOS.Services.CloseAppService>();
#endif

            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(BorderlessEntry), typeof(Platforms.Android.Handler.BorderlessEntryHandler));
#elif IOS || MACCATALYST  || WINDOWS
                handlers.AddHandler(typeof(BorderlessEntry), typeof(BorderlessEntryHandler));
#endif
            });
#if ANDROID
                EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), Platforms.Android.Handler.BorderlessEntryHandler.MapCustomProperties);
#endif
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

#if WINDOWS
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;
#endif
    }
}
