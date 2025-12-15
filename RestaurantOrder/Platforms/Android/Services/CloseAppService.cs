using RestaurantOrder.Platforms.Android.Services;

[assembly: Dependency(typeof(CloseAppService))]
namespace RestaurantOrder.Platforms.Android.Services
{
    //internal class CloseAppService : ICloseAppService

    //{
    //}
    class CloseAppService : ICloseAppService
    {
        public void CloseApp()
        {
            //try
            //{
            //    Android.App.Activity activity = (Android.App.Activity)Platform.CurrentActivity;
            //    activity?.FinishAffinity(); // closes all activities
            //}
            //catch (Exception ex)
            //{
            //    var Error = ex.Message;
            //}

            try
            {
                var activity = Platform.CurrentActivity;
                activity?.FinishAffinity(); // closes all activities

            }
            catch (Exception ex)
            {
                // Handle exception 
            }

        }
    }

}
