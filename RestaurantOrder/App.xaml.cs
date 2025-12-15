using RestaurantOrder.ViewModels.Main;
using RestaurantOrder.Views;

namespace RestaurantOrder
{
    public partial class App : Application
    {
        public static CancellationTokenSource cancellationTokenSource;
        public static MainUserViewModel ObjMainUserViewModel;
        public App()
        {
            InitializeComponent();
            ObjMainUserViewModel = new MainUserViewModel();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
           // return new Window(new AppShell());
            return new Window(new LoginPage());
        }
    }
}