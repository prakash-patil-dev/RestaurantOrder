using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using RestaurantOrder.CustomControls;
using RestaurantOrder.Handlers;
using Syncfusion.Maui.Core.Hosting;

namespace RestaurantOrder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
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
    }
}
