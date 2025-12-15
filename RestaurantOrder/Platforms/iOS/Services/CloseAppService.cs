using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Microsoft.Maui.Controls.Dependency(typeof(RestaurantOrder.Platforms.iOS.Services.CloseAppService))]

namespace RestaurantOrder.Platforms.iOS.Services
{
    public class CloseAppService : ICloseAppService
    {
        public void CloseApp()
        {
            // Not recommended for App Store apps
            Thread.CurrentThread.Abort();
        }
    }


}
